using System;
using DevToDev.Application.Common.Interfaces;

namespace DevToDev.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}