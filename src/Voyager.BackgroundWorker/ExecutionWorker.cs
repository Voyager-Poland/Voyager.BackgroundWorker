namespace Voyager.BackgroundWorker
{
	internal sealed partial class WorkerProcessor
	{
		class ExecutionWorker
		{
			private readonly WorkerTask workerTask;

			public ExecutionWorker(WorkerTask workerTask)
			{
				this.workerTask = workerTask;
			}

			public virtual void Execute(CancellationToken cancellationToken)
			{
				workerTask.Run(cancellationToken);
			}
		}
	}
}
