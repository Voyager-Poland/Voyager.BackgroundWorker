using System;
using System.Threading;

namespace Voyager.BackgroundWorker
{
	internal class Guard
	{
		readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

		public void ProcessAction(Action<CancellationToken> action, CancellationToken cancellationToken)
		{
			if (!GetAvalableToEnter(cancellationToken))
				return;
			try
			{
				action.Invoke(cancellationToken);
			}
			finally
			{
				Release();
			}
		}

		protected virtual bool GetAvalableToEnter(CancellationToken token)
		{
			try
			{
				return semaphoreSlim.Wait(1000, token);
			}
			catch (OperationCanceledException)
			{
				return false;
			}
		}

		private void Release() => semaphoreSlim.Release();
	}
}