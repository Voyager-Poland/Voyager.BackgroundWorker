namespace Voyager.BackgroundWorker.Test
{
	public class ExternalLockTest : SimpleExampleTest
	{
		ExternalLock myLockObject;

		[SetUp]
		public void CreateLock()
		{
			myLockObject = new MyLock(this);
		}
		protected override void SetBuilder(BackgroundBuilder builder)
		{
			base.SetBuilder(builder);
			builder.AddExternalLock((prov) => { return this.myLockObject; });
		}

		[Test]
		public override async Task RunService()
		{
			await base.RunService();
			Assert.That(enterCount, Is.GreaterThan(1)); ;
			Assert.That(enterCount, Is.EqualTo(exitCcount));
		}

		class MyLock : ExternalLock
		{
			private ExternalLockTest host;

			public MyLock(ExternalLockTest host)
			{
				this.host = host;
			}

			bool inn = false;
			public override void BeginLock()
			{
				if (inn)
					throw new Exception("Nie można wejść!");
				base.BeginLock();
				inn = true;
				this.host.SetEnter();
			}

			public override void EndLock()
			{
				base.EndLock();
				inn = false;
				this.host.SetEnd();
			}
		}
		int enterCount;
		int exitCcount;

		private void SetEnd()
		{
			exitCcount++;
		}

		private void SetEnter()
		{
			enterCount++;
		}
	}
}
