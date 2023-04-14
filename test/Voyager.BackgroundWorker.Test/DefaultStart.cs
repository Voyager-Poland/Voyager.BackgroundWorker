namespace Voyager.BackgroundWorker.Test
{
	public class DefaultStart
	{
		private IHost host;

		[SetUp]
		public void SetupService()
		{
			var appBuilder = Host.CreateDefaultBuilder(null);
			appBuilder.ConfigureServices((services) =>
			{
				var builder = services.AddBackgroundWorker();
				SetBuilder(builder);
			});
			AdditionConfiguration(appBuilder);
			host = appBuilder.Build();
			host.Start();
		}

		[TearDown]
		public void TearDown()
		{
			host.Dispose();
		}

		protected virtual void AdditionConfiguration(IHostBuilder appBuilder)
		{

		}

		protected virtual void SetBuilder(BackgroundBuilder builder)
		{
		}

		[Test]
		public virtual Task RunService()
		{
			return Task.CompletedTask;
		}
	}
}
