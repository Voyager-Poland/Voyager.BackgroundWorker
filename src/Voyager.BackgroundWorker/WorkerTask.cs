using System.Threading;

namespace Voyager.BackgroundWorker
{
	public interface WorkerTask
	{
		void Run(CancellationToken cancellationToken);
	}
}
