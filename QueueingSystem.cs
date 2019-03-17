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
        private double maxServingTime = 4;

        public int MaxRequestCount { get; set; }

        public QueueingSystem(double lambda)
        {
            this.MaxRequestCount = 1000;
            this.lambda = lambda;
        }

        public ModelingResult Run()
        {
            var waitingSum = 0.0;
            var downtimesSum = 0.0;
            var spentTimesSum = 0.0;
            var maxQueueSize = 0;
            var queueSizesSum = 0L;
            var requestTimes = this.GenerateRequestTimes();
            var waitingRequests = new Queue<double>();
            var currentTime = 0.0;
            while (true) {
                double servicedRequest;
                if (waitingRequests.Any()) {
                    servicedRequest = waitingRequests.Dequeue();
                    queueSizesSum += waitingRequests.Count;
                    var waitingTime = currentTime - servicedRequest;
                    waitingSum += waitingTime;
                } else if (requestTimes.Any()) {
                    servicedRequest = requestTimes.Dequeue();
                    Debug.Assert(servicedRequest >= currentTime, "Queueing error");
                    downtimesSum += servicedRequest - currentTime;
                    currentTime = servicedRequest;
                } else {
                    break;
                }
                var serviceTime = this.GenerateSeviceTime();
                var endService = currentTime + serviceTime;
                spentTimesSum += endService - servicedRequest;
                while (requestTimes.Any() && requestTimes.Peek() < endService) {
                    waitingRequests.Enqueue(requestTimes.Dequeue());
                    queueSizesSum += waitingRequests.Count;
                }
                currentTime = endService;
                maxQueueSize = Math.Max(maxQueueSize, waitingRequests.Count);
            }
            return new ModelingResult
            {
                MaxQueueSize = maxQueueSize,
                AverageDowntime = downtimesSum / MaxRequestCount,
                AverageWaitingInQueue = waitingSum / MaxRequestCount,
                AverageSpentTime = spentTimesSum / MaxRequestCount,
                AverageQueueSize = queueSizesSum / MaxRequestCount
            };
        }

        private Queue<double> GenerateRequestTimes()
        {
            var times = new Queue<double>(this.MaxRequestCount);
            var previous = 0.0;
            while (times.Count < this.MaxRequestCount)
            {
                var randomValue = QueueingSystem.rnd.NextDouble();
                var delay = -Math.Log(randomValue) / this.lambda;
                var nextRequest = previous + delay;
                times.Enqueue(nextRequest);
                previous = nextRequest;
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