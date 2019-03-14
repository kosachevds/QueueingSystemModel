namespace QueueingSystemModel
{
    class ModelingResult
    {
        public double AverageExistingTime { get; set; }
        public double AverageWaitingInQueue { get; set; }
        public double AverageDowntime { get; set; }
        public double MaxQueueSize {get; set;}
    }
}