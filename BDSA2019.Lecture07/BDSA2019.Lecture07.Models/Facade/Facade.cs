using System;

namespace BDSA2019.Lecture07.Models.Facade
{
    public class Facade
    {
        private static readonly Notifier _notifier = new Notifier();
        private static readonly Publisher _publisher = new Publisher();
        private static readonly Archiver _archiver = new Archiver();
        private static readonly PeopleRepository _peopleRepository = new PeopleRepository();

        public void Publish(Article article)
        {
            Console.WriteLine("Publishing");
            _publisher.PublishOnline(article);

            Console.WriteLine("Archiving");
            _archiver.Archive(article);

            var people = _peopleRepository.All();

            Console.WriteLine("Notifying");
            _notifier.Notify(article, people);
        }
    }
}
