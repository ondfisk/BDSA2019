using System;

namespace BDSA2019.Lecture07.Models.Facade
{
    public class Publisher
    {
        public void PublishOnline(Article article)
        {
            Console.WriteLine($"Published {article.Title} by {article.Author} online");
        }
    }
}
