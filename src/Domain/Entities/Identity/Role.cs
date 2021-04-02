using System.Collections.Generic;

namespace DevToDev.Domain.Entities.Identity
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new List<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public IList<User> Users { get; set; }
    }
}