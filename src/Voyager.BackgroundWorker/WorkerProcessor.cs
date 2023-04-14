namespace Voyager.BackgroundWorker
{
	internal sealed class WorkerProcessor
	{
		readonly IServiceProvider _provider;


		public WorkerProcessor(IServiceProvider provider)
		{
			_provider = provider;
		}

		public Task ServiceRun(CancellationToken cancellationToken)
		{
			return Task.Run(() =>
				{
					using var scope = _provider.CreateScope();
					var serviceProvider = scope.ServiceProvider;
					InstanceOfTask myTask = new(serviceProvider.GetService<WorkerTask>()!, serviceProvider.GetService<ExternalLock>()!, serviceProvider.GetService<Guard>()!);
					myTask.Execute(cancellationToken);
				});
		}

		class InstanceOfTask
		{
			private WorkerTask mainProcess;
			private ExternalLock externalLock;
			private Guard semafor;

			public InstanceOfTask(WorkerTask mainProcess, ExternalLock serviceLock, Guard semafor)
			{
				this.mainProcess = mainProcess;
				this.externalLock = serviceLock;
				this.semafor = semafor;
			}

			public void Execute(CancellationToken cancellationToken)
			{
				semafor.ProcessAction(MakeLockAndCallAction, cancellationToken);
			}

			void MakeLockAndCallAction(CancellationToken token)
			{
				externalLock.RunProtectedTask(mainProcess, token);
			}
		}
	}
}
