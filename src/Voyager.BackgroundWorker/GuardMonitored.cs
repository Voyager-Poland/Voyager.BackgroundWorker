namespace Voyager.BackgroundWorker
{
	internal class GuardMonitored : Guard
	{
		readonly ILogger logger;

		public GuardMonitored(ILogger<Guard> logger)
		{
			this.logger = logger;
		}

		protected override bool GetAvalableToEnter(CancellationToken token)
		{
			var access = base.GetAvalableToEnter(token);
			if (!access)
				logger.LogInformation("The previous task hasn't been finished yet.");
			return access;
		}
	}

}
