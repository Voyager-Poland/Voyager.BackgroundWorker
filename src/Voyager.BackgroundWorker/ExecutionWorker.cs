using System.Threading;

namespace Voyager.BackgroundWorker
{
	public class ExecutionWorker
	{
		private readonly WorkerTask workerTask;

		public ExecutionWorker(WorkerTask workerTask)
		{
			this.workerTask = workerTask;
		}

		public virtual void Execute(CancellationToken cancellationToken) => workerTask.Run(cancellationToken);
	}
}

