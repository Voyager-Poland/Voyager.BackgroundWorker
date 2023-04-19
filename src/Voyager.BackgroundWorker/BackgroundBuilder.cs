using Microsoft.Extensions.DependencyInjection;

namespace Voyager.BackgroundWorker
{
	public class BackgroundBuilder
	{
		internal BackgroundBuilder(IServiceCollection services)
		{
			Services = services;
		}
		public IServiceCollection Services { get; }
	}
}
