using System;
using System.Collections.Generic;

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
            var times = this.GenerateTimes();
        }

        private List<double> GenerateTimes()
        {
            var times = new List<double>(this.MaxRequestCount);
            var last = 0.0;
            while (times.Count < this.MaxRequestCount)
            {
                var newItem = -Math.Log(rnd.NextDouble()) / this.lambda;
                last = newItem;
                times.Add(last + newItem);
            }
            return times;
        }

    }
}