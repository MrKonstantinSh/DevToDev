using System;

namespace DevToDev.Domain.Entities.Identity
{
    public class UserDetails : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}