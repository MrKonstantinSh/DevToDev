using System;

namespace DevToDev.Domain.Entities.Identity
{
    public class RevokedToken : BaseEntity
    {
        public string AccessToken { get; set; }
        public DateTime RevocationDate { get; set; }
    }
}