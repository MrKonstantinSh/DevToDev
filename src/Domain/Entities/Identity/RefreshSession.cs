using System;

namespace DevToDev.Domain.Entities.Identity
{
    public class RefreshSession : BaseEntity
    {
        public string RefreshToken { get; set; }
        public string UserAgent { get; set; }
        public string Fingerprint { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresIn { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}