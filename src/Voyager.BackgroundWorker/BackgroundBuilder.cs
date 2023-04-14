namespace Voyager.BackgroundWorker
{
	public class BackgroundBuilder
	{
		public BackgroundBuilder(IServiceCollection services)
		{
			Services = services;
		}
		public IServiceCollection Services { get; }
	}
}
