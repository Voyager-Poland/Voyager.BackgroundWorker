using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace Voyager.BackgroundWorker
{
	internal sealed class WorkerProcessor
	{
		readonly IServiceScopeFactory factory;

		public WorkerProcessor(IServiceScopeFactory factory)
		{
			this.factory = factory;
		}

		public Task ServiceRun(CancellationToken cancellationToken)
		{
			return Task.Run(() =>
				{
					using var scope = factory.CreateScope();
					var serviceProvider = scope.ServiceProvider;
					ExecutionWorker myTask = serviceProvider.GetService<ExecutionWorker>()!;
					myTask.Execute(cancellationToken);
				}, cancellationToken);
		}
	}
}
