namespace Voyager.BackgroundWorker.Test
{

	public class SimpleExampleTest : DefaultStart, WorkerTask
	{
		private EventWaitHandle enventHandle;

		[SetUp]
		public void Setup()
		{
			call = 0;
			enventHandle = new ManualResetEvent(false);
		}

		[Test]
		public override Task RunService()
		{
			var wejscie = enventHandle.WaitOne(30000);
			Assert.That(wejscie, Is.True);
			Assert.That(call, Is.GreaterThan(0));
			return Task.CompletedTask;
		}

		protected override void SetBuilder(BackgroundBuilder builder)
		{
			builder.AddWorker((prov) => { return this; }).SetTimeUpToWakeUp(1);
		}

		volatile int call = 0;
		public void Run(CancellationToken cancellationToken)
		{
			Interlocked.Add(ref call, 1);
			if (call > 1)
				this.enventHandle.Set();
			Console.WriteLine($"Zadanie wykonane po raz {call}");
		}

		/*

		void fakeReq()
		{
			var appBuilder = Host.CreateDefaultBuilder(null);
			appBuilder.ConfigureServices((services) =>
					{
						services.AddBackgroundWorker().AddWorker<MyTask>();
					});
			host = appBuilder.Build();
			host.Start();

		}

		void fakeReqExternal()
		{
			var appBuilder = Host.CreateDefaultBuilder(null);
			appBuilder.ConfigureServices((services) =>
			{
				services.AddBackgroundWorker().AddWorker<MyTask>().AddExternalLock<MyLock>();
			});
			host = appBuilder.Build();
			host.Start();

		}

		void fakeReqWeb()
		{
			var appBuilder = Host.CreateDefaultBuilder(null);
			appBuilder.ConfigureServices((services) =>
			{
				services.AddBackgroundWorker().AddWorker<MyTask>();
			});
			appBuilder.ConfigureWebHostDefaults((webConfigure) =>
			{
				webConfigure.UseStartup<Startup>();
			});
			host = appBuilder.Build();
			host.Start();
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


		void fakeReq2()
		{
			var appBuilder = Host.CreateDefaultBuilder(null);
			appBuilder.ConfigureServices((services) =>
			{
				services.AddBackgroundWorker().AddWorker<MyTask>().SetTimeUpToWakeUp(300);
			});
			host = appBuilder.Build();
			host.Start();

		}

		public class MyTask : Voyager.BackgroundWorker.WorkerTask
		{
			public void Run(CancellationToken cancellationToken)
			{
				//The logic goes here!
			}
		}
		*/
	}
}