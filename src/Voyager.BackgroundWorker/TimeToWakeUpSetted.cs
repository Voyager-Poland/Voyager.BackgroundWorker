using System;

namespace Voyager.BackgroundWorker
{
	public class TimeToWakeUpSetted : TimeToWakeUp
	{
		int timeToWakaUseSec;
		public TimeToWakeUpSetted(int timeToWakapeSec)
		{
			this.timeToWakaUseSec = timeToWakapeSec;
		}

		public override bool GetCallDecision(DateTime lastCallTime)
		{
			TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks - lastCallTime.Ticks);
			if (timeSpan.TotalSeconds > timeToWakaUseSec)
				return true;
			return false;
		}
	}
}
