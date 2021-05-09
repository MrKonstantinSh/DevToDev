using System;
using DevToDev.Domain.Entities.Identity;

namespace DevToDev.Domain.Entities.Article
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfLastUpdate { get; set; }

        public User User { get; set; }
    }
}