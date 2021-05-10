using System;

namespace DevToDev.Application.Article.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}