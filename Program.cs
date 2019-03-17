using System;

namespace QueueingSystemModel
{
    class Program
    {
        static void Main(string[] args)
        {
            RunModeling();
        }

        static void RunModeling()
        {
            var queuingSystem = new QueueingSystem(0.5);
            var results = queuingSystem.Run();
            Console.WriteLine($"Results:\n{ results }");
        }
    }
}
