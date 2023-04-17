namespace Voyager.BackgroundWorker
{
	internal class StopApplicationProcess
	{
		private readonly IHostApplicationLifetime hostApplicationLifetime;

		public StopApplicationProcess(IHostApplicationLifetime hostApplicationLifetime)
		{
			this.hostApplicationLifetime = hostApplicationLifetime;
		}

		public void StopApplication()
		{
			hostApplicationLifetime.StopApplication();
		}
	}
}
