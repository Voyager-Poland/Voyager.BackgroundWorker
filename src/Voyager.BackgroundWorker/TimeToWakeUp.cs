using System;

namespace Voyager.BackgroundWorker
{
	public class TimeToWakeUp
	{
		public virtual bool GetCallDecision(DateTime lastCallTime, System.Threading.CancellationToken stoppingToken)
		{
			return false;
		}
	}
}
