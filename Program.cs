using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QueueingSystemModel
{
    class Program
    {
        const string OutputFilename = "temp.txt";

        static void Main(string[] args)
        {
            SetInvariantCulture();
            // RunSystem();
            AnalyzeFluxDensity().Wait();
        }

        static void SetInvariantCulture() {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.InvariantCulture;
        }

        static void RunSystem()
        {
            var queuingSystem = new QueueingSystem(0.5);
            var results = queuingSystem.Run();
            Console.WriteLine($"Results:\n{ results }");
        }

        static async Task AnalyzeFluxDensity()
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
            await WriteResultsAsync(OutputFilename, lambdas, results);
        }

        static async Task WriteResultsAsync(string filename, List<double> variable, List<ModelingResult> results)
        {
            using (var writer = File.CreateText(filename))
            {
                for (int i = 0; i < variable.Count; ++i)
                {
                    var newLine = String.Format("{0} {1}", variable[i], results[i].ToSimpleString());
                    await writer.WriteLineAsync(newLine);
                }
            }
        }
    }
}
