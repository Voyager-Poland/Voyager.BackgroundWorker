namespace Voyager.BackgroundWorker
{
	internal sealed class WorkerService : BackgroundService
	{
		ProcessingJobs processing;

		public WorkerService(ProcessingJobs processing)
		{
			this.processing = processing;
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