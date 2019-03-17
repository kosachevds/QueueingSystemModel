using System;

namespace QueueingSystemModel
{
    class ModelingResult
    {
        public double AverageSpentTime { get; set; }
        public double AverageWaitingInQueue { get; set; }
        public double AverageDowntime { get; set; }
        public double MaxQueueSize { get; set; }
        public double AverageQueueSize { get; set; }

        public override string ToString()
        {
            var template = "Average Spent Time: {0}, Average Waiting Time: {1}, " +
                "Average Downtime: {2}, Max Queue Size: {3}, Average queue size: {4}";
            return ToStringWithFormat(template);
        }

        public string ToSimpleString()
        {
            return ToStringWithFormat("{0} {1} {2} {3} {4}");
        }

        private string ToStringWithFormat(string formatString)
        {
            return String.Format(formatString,
                AverageSpentTime, AverageWaitingInQueue, AverageDowntime,
                MaxQueueSize, AverageQueueSize);
        }
    }
}