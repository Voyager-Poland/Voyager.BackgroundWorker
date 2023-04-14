using Voyager.BackgroundWorker;

namespace Microsoft.AspNetCore.Builder
{
	public static class BuilderWorker
	{
		public static IEndpointRouteBuilder MapBackgroundWorker(this IEndpointRouteBuilder endpointRoute, string pattern = "/run")
		{

			endpointRoute.MapGet(pattern, () =>
			{
				var worker = endpointRoute.ServiceProvider.GetService<WorkerProcessor>()!;
				worker.ServiceRun(System.Threading.CancellationToken.None);
				return "OK";
			});


			return endpointRoute;
		}
	}
}
