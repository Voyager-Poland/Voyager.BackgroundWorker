using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Voyager.BackgroundWorker
{
	internal sealed class WorkerService : BackgroundService
	{
		private readonly IServiceScopeFactory serviceScope;

		public WorkerService(IServiceScopeFactory serviceScope)
		{
			this.serviceScope = serviceScope;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			return Task.Run(() =>
			{
				using var scope = serviceScope.CreateScope();
				ProcessingJobs processing = scope.ServiceProvider.GetService<ProcessingJobs>()!;
				processing.StartService(stoppingToken);
			});
		}
	}

}