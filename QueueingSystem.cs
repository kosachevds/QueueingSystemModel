using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QueueingSystemModel
{
    class QueueingSystem
    {
        private static Random rnd = new Random();
        private double lambda;
        private double minServingTime = 0.1;
        private double maxServingTime = 10;

        public int MaxRequestCount { get; set; }

        public QueueingSystem(double lambda)
        {
            this.MaxRequestCount = 1000;
            this.lambda = lambda;
        }

        public ModelingResult Run()
        {

        }

        private Queue<double> GenerateTimes()
        {
            var times = new Queue<double>(this.MaxRequestCount);
            var last = 0.0;
            while (times.Count < this.MaxRequestCount)
            {
                var randomValue = QueueingSystem.rnd.NextDouble();
                var newItem = -Math.Log(randomValue) / this.lambda;
                last = newItem;
                times.Enqueue(last + newItem);
            }
            return times;
        }

        private double GenerateSeviceTime()
        {
            var randomValue = QueueingSystem.rnd.NextDouble();
            return this.minServingTime +
                randomValue * (this.maxServingTime - this.minServingTime);
        }

        private static double

    }
}