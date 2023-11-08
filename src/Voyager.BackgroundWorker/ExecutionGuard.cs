using System.Threading;

namespace Voyager.BackgroundWorker
{
	class ExecutionGuard : ExecutionExternalLock
	{
		private Guard guard;

		public ExecutionGuard(WorkerTask workerTask, ExternalLock externalLock, Guard guard) : base(workerTask, externalLock)
		{
			this.guard = guard;
		}

		public override void Execute(CancellationToken cancellationToken)
		{
			guard.ProcessAction(base.Execute, cancellationToken);
		}
	}
}
