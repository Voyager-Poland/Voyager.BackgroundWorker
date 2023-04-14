namespace Voyager.BackgroundWorker
{
	internal sealed class WorkerService : BackgroundService
	{
		ProcessingJobs processing;
		IHostApplicationLifetime hostApplicationLifetime;

		public WorkerService(WorkerProcessor worker, ILogger<WorkerService> logger, IHostApplicationLifetime hostApplicationLifetime, TimeToWakeUp timeToWakeUp)
		{
			this.hostApplicationLifetime = hostApplicationLifetime;
			processing = logger != null ? new ProcessingJobMonitored(worker, timeToWakeUp, StopApplication, logger) : new ProcessingJobs(worker, timeToWakeUp, StopApplication);
		}

		private void StopApplication()
		{
			hostApplicationLifetime.StopApplication();
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			return Task.Run(() =>
			{
				processing.StartService(stoppingToken);
			});
		}
	}

}