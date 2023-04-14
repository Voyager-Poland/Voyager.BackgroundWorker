namespace Voyager.BackgroundWorker.Defaults
{
	internal class BlindTask : WorkerTask
	{
		public void Run(CancellationToken cancellationTocken)
		{
			Console.WriteLine("This is only BlindTask!");
		}
	}
}
