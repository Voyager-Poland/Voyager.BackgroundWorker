namespace Voyager.BackgroundWorker
{
	internal sealed class WorkerProcessor
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
					ExecutionWorker myTask = serviceProvider.GetService<ExecutionWorker>()!;
					myTask.Execute(cancellationToken);
				});
		}
	}
}
