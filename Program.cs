using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueingSystemModel
{
    class Program
    {
        const string OutputFilename = "temp.txt";

        static void Main(string[] args)
        {
            RunSystem();
        }

        static void RunSystem()
        {
            var queuingSystem = new QueueingSystem(0.5);
            var results = queuingSystem.Run();
            Console.WriteLine($"Results:\n{ results }");
        }

        static void AnalyzeFluxDensity()
        {
            const double MinLambda = 0.1;
            const double MaxLambda = 4;
            const double LambdaStep = 0.1;

            var queuingSystem = new QueueingSystem(MinLambda);
            var lambdas = new List<double>();
            var results = new List<ModelingResult>();
            while (queuingSystem.Lambda < MaxLambda)
            {
                var result = queuingSystem.Run();
                results.Add(result);
                lambdas.Add(queuingSystem.Lambda);
                queuingSystem.Lambda += LambdaStep;
            }
            WriteResults(OutputFilename, lambdas, results);
        }

        static void WriteResults(string filename, List<double> variable, List<ModelingResult> results)
        {

        }
    }
}
