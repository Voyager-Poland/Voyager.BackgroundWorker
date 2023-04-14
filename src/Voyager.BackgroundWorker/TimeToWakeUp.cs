namespace Voyager.BackgroundWorker
{
	public class TimeToWakeUp
	{
		public virtual bool GetCallDecision(DateTime lastCallTime)
		{
			return false;
		}
	}

	internal class TimeToWakeUpSetted : TimeToWakeUp
	{
		int timeToWakaUseSec;
		public TimeToWakeUpSetted(int timeToWakapeSec)
		{
			this.timeToWakaUseSec = timeToWakapeSec;
		}

		public override bool GetCallDecision(DateTime lastCallTime)
		{
			TimeSpan timeSpan = new(DateTime.Now.Ticks - lastCallTime.Ticks);
			if (timeSpan.TotalSeconds > timeToWakaUseSec)
				return true;
			return false;
		}
	}
}
