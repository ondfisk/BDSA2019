using BDSA2019.Lecture07.Models.ChainOfResponsibility.ATM;
using Microsoft.Extensions.DependencyInjection;

namespace BDSA2019.Lecture07.App
{
    class Program
    {
        static void Main(string[] args)
        {
            StrategyContainer.Run();

            // ATM.Run();
        }
    }
}
