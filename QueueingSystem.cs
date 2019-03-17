using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            var waitingTimes = new List<double>(MaxRequestCount);
            var downtimes = new List<double>(MaxRequestCount);
            var maxQueueSize = 0;
            var times = this.GenerateTimes();
            var waitingRequests = new Queue<double>();
            var currentTime = 0.0;
            while (true) {
                double servicedRequest;
                if (waitingRequests.Count > 0) {
                    servicedRequest = waitingRequests.Dequeue();
                    waitingTimes.Add(currentTime - servicedRequest);
                    downtimes.Add(0.0);
                } else if (times.Count > 0) {
                    servicedRequest = times.Dequeue();
                    Debug.Assert(servicedRequest >= currentTime, "Queueing error");
                    waitingTimes.Add(0.0);
                    downtimes.Add(servicedRequest - currentTime);
                    currentTime = servicedRequest;
                } else {
                    break;
                }
                var serviceTime = this.GenerateSeviceTime();
                var endService = currentTime + serviceTime;
                while (times.Peek() < endService) {
                    waitingRequests.Enqueue(times.Dequeue());
                }
                maxQueueSize = Math.Max(maxQueueSize, waitingRequests.Count);
            }
            return new ModelingResult
            {
                MaxQueueSize = maxQueueSize,
                AverageDowntime = GetAverage(downtimes),
                AverageWaitingInQueue = GetAverage(waitingTimes)
            };
        }

        private Queue<double> GenerateTimes()  // Rename to GenerateRequests
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

        static double GetAverage(List<double> values)
        {
            return values.Sum() / values.Count;
        }
    }
}