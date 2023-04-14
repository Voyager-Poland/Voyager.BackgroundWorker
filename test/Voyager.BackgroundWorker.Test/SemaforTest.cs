namespace Voyager.BackgroundWorker.Test
{
	public class SemaforTest
	{
		private Guard semafore;

		[SetUp]
		public void SetupSemafore()
		{
			countNumber = 0;
			semafore = new Voyager.BackgroundWorker.Guard();
		}

		volatile int countNumber = 0;

		[Test]
		public void NormalRun()
		{
			semafore.ProcessAction(MockCall, CancellationToken.None);
			Assert.That(countNumber, Is.EqualTo(1));
		}


		[Test]
		public void ManyRun()
		{
			var t1 = Task.Run(() => semafore.ProcessAction(MockCall, CancellationToken.None));
			var t2 = Task.Run(() => semafore.ProcessAction(MockCall, CancellationToken.None));
			var t3 = Task.Run(() => semafore.ProcessAction(MockCall, CancellationToken.None));

			Task.WaitAll(t1, t2, t3);

			Assert.That(countNumber, Is.EqualTo(1));
		}

		void MockCall(CancellationToken cancellationToken)
		{
			Thread.Sleep(2000);
			Interlocked.Add(ref countNumber, 1);
		}
	}
}
