namespace Voyager.BackgroundWorker
{
	internal sealed partial class WorkerProcessor
	{
		readonly IServiceProvider provider;

		public WorkerProcessor(IServiceProvider provider)
		{
			this.provider = provider;
		}

		public Task ServiceRun(CancellationToken cancellationToken)
		{
			return Task.Run(() =>
				{
					using var scope = provider.CreateScope();
					var serviceProvider = scope.ServiceProvider;
					ExecutionWorker myTask = new ExecutionGuard(serviceProvider.GetService<WorkerTask>()!, serviceProvider.GetService<ExternalLock>()!, serviceProvider.GetService<Guard>()!);
					myTask.Execute(cancellationToken);
				});
		}
	}
}
