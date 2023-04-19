using System;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("Voyager.BackgroundWorker.Test, PublicKey=00240000048000009400000006020000002400005253413100040000010001009dfc3f2e8d01b520d7b877cd95fd568c89b9b12489b622ba4ca00fdbe8bb60b1a39ff2f7f8dcdd6e73ed25013750c4e97a1eb5926f4654b42abd7465a509cc0660b72fa7bd5842f3272eb915394b4f20586c4a85efb5d2fdacb5001609171e7c0e178ae09c162331bba3ba5bb818eac82eac97b5313cc63677056b3c9247c0bb")]

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

		private void Release()
		{
			semaphoreSlim.Release();
		}
	}

}