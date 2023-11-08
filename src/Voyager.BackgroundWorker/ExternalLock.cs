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
				Call(action, cancellationToken);
			}
			finally
			{
				EndLock();
			}
		}

		protected virtual void Call(Action<CancellationToken> action, CancellationToken cancellationToken)
		{
			action.Invoke(cancellationToken);
		}

		public virtual void BeginLock() { }

		public virtual void EndLock() { }
	}
}
