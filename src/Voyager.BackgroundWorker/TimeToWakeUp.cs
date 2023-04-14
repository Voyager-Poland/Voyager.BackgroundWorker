namespace Voyager.BackgroundWorker
{
	public class TimeToWakeUp
	{
		public virtual bool GetCallDecision(DateTime lastCallTime)
		{
			return false;
		}
	}
}
