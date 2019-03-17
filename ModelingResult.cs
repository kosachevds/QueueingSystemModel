using System;

namespace QueueingSystemModel
{
    class ModelingResult
    {
        public double AverageSpentTime { get; set; }
        public double AverageWaitingInQueue { get; set; }
        public double AverageDowntime { get; set; }
        public double MaxQueueSize { get; set; }

        public override string ToString()
        {
            var template = "Average Spent Time: {0}, Average Waiting Time: {1}, " +
                "Average Downtime: {2}, Max Queue Size: {3}";
            return String.Format(template,
                AverageSpentTime, AverageWaitingInQueue, AverageDowntime, MaxQueueSize);
        }
    }
}