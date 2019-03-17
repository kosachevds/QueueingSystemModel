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
            return $@"Average Spent Time: {AverageSpentTime}, \
            Average Waiting Time: {AverageWaitingInQueue}, \
            Average Downtime: {AverageDowntime}, \
            Max Queue Size: {MaxQueueSize}";
        }
    }
}