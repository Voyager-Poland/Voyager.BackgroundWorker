using System;
using System.Threading;

namespace Voyager.BackgroundWorker
{
	public class ExternalLock
	{
		public void RunProtectedTask(Action<CancellationToken> action, CancellationToken cancellationToken)
		{
			BeginLock();
			try
			{
				action.Invoke(cancellationToken);
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
