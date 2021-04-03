using System;

namespace DevToDev.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        public DateTime UtcNow { get; }
    }
}