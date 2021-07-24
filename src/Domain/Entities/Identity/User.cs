using System;
using System.Collections.Generic;

namespace DevToDev.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public User()
        {
            Roles = new List<Role>();
            RefreshSessions = new List<RefreshSession>();
            Articles = new List<Article.Article>();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationDate { get; set; }
        public bool IsEmailVerified => EmailVerificationDate.HasValue;

        public UserDetails UserDetails { get; set; }
        public IList<Role> Roles { get; set; }
        public IList<RefreshSession> RefreshSessions { get; set; }
        public IList<Article.Article> Articles { get; set; }
    }
}