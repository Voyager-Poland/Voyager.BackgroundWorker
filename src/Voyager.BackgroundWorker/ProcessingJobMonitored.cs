namespace Voyager.BackgroundWorker
{
	class ProcessingJobMonitored : ProcessingJobs
	{
		private readonly ILogger logger;

		public ProcessingJobMonitored(WorkerProcessor worker, TimeToWakeUp timeToWakeUp, Action onTermianteCall, ILogger logger) : base(worker, timeToWakeUp, onTermianteCall)
		{
			this.logger = logger;
		}

		protected override void ProcessOneTask(CancellationToken stoppingToken)
		{
			logger.LogInformation("WackUp");
			base.ProcessOneTask(stoppingToken);
		}

		protected override void HandlingException(Exception ex)
		{
			logger.LogError(ex, "Main loop");
		}
	}
}
