using BDSA2019.Lecture07.App;
using BDSA2019.Lecture08.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BDSA2019.Lecture08
{
    public class Program
    {
        private static IServiceProvider Container => IoCContainer.Container;

        static void Main(string[] args)
        {
            // Threads.SpawnThread();
            // Threads.SpawnMultipleThreads(100000);
            // Threads.Overlapping();
            //Threads.OverlappingWithArguments();
            // Threads.Join();

            // RaceCondition.Race();
            // FixedRace.Race();
            // BehindTheScenes.Race();

            // Deadlock.Run();
            // Deadlock.RunWithComments();
            // Deadlock.RunWithCommentsAndOrder();

            // Tasks.TaskFactory();
            // Tasks.Wait();
            //Tasks.WaitAll();
            // Tasks.Attached();
            //Tasks.Continuation();
            //Tasks.Result();
            // Tasks.Cancellation();
            // Tasks.ResultCancelled();
            // Tasks.Fail();

            // TaskParallelLibrary.For();
            // TaskParallelLibrary.ForEach();
            // TaskParallelLibrary.Invoke();

            // ParallelLinq.Run();

            ConcurrentCollections.Race();

            // var repo = Container.GetService<ISuperheroRepository>();

            // foreach (var hero in repo.Read())
            // {
            //     Console.WriteLine($"{hero.Name} aka {hero.AlterEgo}");
            // }

            // Console.WriteLine("Press any key to continue...");
            // Console.Read();
        }
    }
}
