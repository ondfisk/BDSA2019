using BDSA2019.Lecture07.Models.Bridge;
using BDSA2019.Lecture07.Models.ChainOfResponsibility.Approval;
using BDSA2019.Lecture07.Models.ChainOfResponsibility.ATM;
using BDSA2019.Lecture07.Models.Facade;
using BDSA2019.Lecture07.Models.FactoryMethod;
using BDSA2019.Lecture07.Models.IoCContainer;
using BDSA2019.Lecture07.Models.Iterator;
using BDSA2019.Lecture07.Models.Singleton;
using BDSA2019.Lecture07.Models.Strategy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace BDSA2019.Lecture07.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = IoCContainer.Container;

            // var service = container.GetService<IAnimalService>();

            // service.Speak();

            // Game.Run();

            // var config = container.GetService<IConfig>();

            // System.Console.WriteLine(config.ClientId);

            var article = new Article
            {
                Author = "Woodward and Bernstein",
                Body = "Lots of stuff about a crook",
                Title = "Deep Throat"
            };

            var facade = container.GetRequiredService<Facade>();
            // facade.Publish(article);

            // StrategyContainer.Run();

            var repo = container.GetService<ICharacterRepository>();
            var bridge = new Bridge(repo);

            await bridge.PrintAll();
        }
    }
}
