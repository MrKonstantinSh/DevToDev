using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Exceptions;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;

namespace DevToDev.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
            var attributes = authorizeAttributes as AuthorizeAttribute[] ?? authorizeAttributes.ToArray();

            if (attributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == 0) throw new UnauthorizedAccessException();

                // Role-based authorization
                var authorizeAttributesWithRoles = attributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));
                var attributesWithRoles = authorizeAttributesWithRoles as AuthorizeAttribute[] ??
                                          authorizeAttributesWithRoles.ToArray();

                if (attributesWithRoles.Any())
                    foreach (string[] roles in attributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        bool authorized = false;

                        foreach (string role in roles)
                        {
                            bool isInRole = await _identityService
                                .UserIsInRoleAsync(_currentUserService.UserId, role.Trim());

                            if (isInRole)
                            {
                                authorized = true;

                                break;
                            }
                        }

                        // Must be a member of at least one role in roles
                        if (!authorized) throw new ForbiddenAccessException();
                    }
            }

            // User is authorized or authorization not required
            return await next();
        }
    }
}