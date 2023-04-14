namespace Voyager.BackgroundWorker
{
	public class ExternalLock
	{

		public void RunProtectedTask(WorkerTask serviceTask, CancellationToken cancellationToken)
		{
			BeginLock();
			try
			{
				serviceTask.Run(cancellationToken);
			}
			finally
			{
				EndLock();
			}
		}

		public virtual void BeginLock() { }

		public virtual void EndLock() { }
	}
}
