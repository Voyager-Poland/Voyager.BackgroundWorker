using System.Threading;

namespace Voyager.BackgroundWorker
{
	class ExecutionExternalLock : ExecutionWorker
	{
		private ExternalLock externalLock;
		public ExecutionExternalLock(WorkerTask workerTask, ExternalLock externalLock) : base(workerTask)
		{
			this.externalLock = externalLock;
		}

		public override void Execute(CancellationToken cancellationToken)
		{
			externalLock.RunProtectedTask(base.Execute, cancellationToken);
		}
	}
}
