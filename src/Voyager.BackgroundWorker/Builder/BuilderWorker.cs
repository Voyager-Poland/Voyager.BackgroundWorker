using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Voyager.BackgroundWorker;

namespace Microsoft.AspNetCore.Builder
{
	public static class BuilderWorker
	{
#if OLDFRAME
		public static IEndpointRouteBuilder MapBackgroundWorker(this IEndpointRouteBuilder endpointRoute, string pattern = "/run")
		{
			endpointRoute.MapGet(pattern, (httpcontext) =>
			{
				WorkerProcessor worker = endpointRoute.ServiceProvider.GetService<WorkerProcessor>()!;
				worker.ServiceRun(System.Threading.CancellationToken.None);
				return httpcontext.Response.WriteAsync("Ok");
			});
			return endpointRoute;
		}
#else
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
#endif
	}
}
