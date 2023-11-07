namespace Voyager.BackgroundWorker.Test
{
	public class TimeToWakeUpTest : DefaultStart
	{

		internal static volatile bool someOneCall = false;

		[SetUp]
		public void InitSomeCall()
		{
			someOneCall = false;
		}

		protected override void SetBuilder(BackgroundBuilder builder)
		{
			base.SetBuilder(builder);
			builder.AddTimeToWakeUp<MyTime>();
		}

		[Test]
		public override Task RunService()
		{
			Thread.Sleep(2000);
			Assert.That(someOneCall, Is.True);
			return Task.CompletedTask;
		}


		public class MyTime : Voyager.BackgroundWorker.TimeToWakeUpSetted
		{
			public MyTime() : base(1)
			{
			}

			public override bool GetCallDecision(DateTime lastCallTime, System.Threading.CancellationToken stoppingToken)
			{
				TimeToWakeUpTest.someOneCall = true;
				return base.GetCallDecision(lastCallTime, stoppingToken);
			}
		}
	}
}
