using System;

namespace BDSA2019.Lecture07.Models.Facade
{
    public interface IArchiver
    {
        void Archive(Article article);
    }

    public class Archiver : IArchiver
    {
        public void Archive(Article article)
        {
            Console.WriteLine($"Archived {article.Title} by {article.Author}");
        }
    }
}
