namespace Voyager.BackgroundWorker.Test
{
	public class EndpointTest : SimpleExampleTest
	{
		private HttpClient httpClient;

		[SetUp]
		public void SetUpHttpClient()
		{
			httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri("http://localhost:5000");
		}

		[TearDown]
		public void CloseHtttp()
		{
			httpClient.Dispose();
		}
		protected override void AdditionConfiguration(IHostBuilder appBuilder)
		{
			base.AdditionConfiguration(appBuilder);
			appBuilder.ConfigureWebHostDefaults((webConfigure) =>
			{
				webConfigure.UseStartup<Startup>();
			});
		}

		protected override void SetBuilder(BackgroundBuilder builder)
		{
			base.SetBuilder(builder);
			builder.SetTimeUpToWakeUp(-1);
		}

		[Test]
		public override async Task RunService()
		{
			Task task1 = CallEndPoint();
			Task task2 = CallEndPoint();
			Task task3 = CallEndPoint();

			await Task.WhenAll(task1, task2, task3);
			await base.RunService();
		}

		private Task CallEndPoint()
		{
			return httpClient.GetAsync("/run");
		}

		class Startup
		{
			public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			{
				app.UseRouting();
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapBackgroundWorker();
				});
			}
		}
	}
}
