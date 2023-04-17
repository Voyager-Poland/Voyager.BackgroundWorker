namespace Voyager.BackgroundWorker
{
	internal class ProcessingJobs
	{
		private WorkerProcessor worker;
		private Action onTerminateCall;
		private TimeToWakeUp timeToWakeUp;

		public ProcessingJobs(WorkerProcessor worker, TimeToWakeUp timeToWakeUp, Action onTermianteCall)
		{
			this.worker = worker;
			this.onTerminateCall = onTermianteCall;
			this.timeToWakeUp = timeToWakeUp;
		}

		public void StartService(CancellationToken stoppingToken)
		{
			try
			{
				this.Process(stoppingToken);
			}
			catch (Exception ex)
			{
				try
				{
					HandlingException(ex);
				}
				finally
				{
					onTerminateCall.Invoke();
				}
			}
		}

		void Process(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
				ProcessOneTask(stoppingToken);
		}

		protected virtual void ProcessOneTask(CancellationToken stoppingToken)
		{
			var callTime = DateTime.Now;

			worker.ServiceRun(stoppingToken);

			WaitForReqTime(callTime, stoppingToken);
		}

		protected virtual void HandlingException(Exception ex)
		{
		}

		private void WaitForReqTime(DateTime callTime, CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				if (timeToWakeUp.GetCallDecision(callTime))
					break;
				Thread.Sleep(1000);
			}
		}

	}
}
