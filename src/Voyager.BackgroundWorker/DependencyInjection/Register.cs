using System.Diagnostics.CodeAnalysis;
using Voyager.BackgroundWorker;
using Voyager.BackgroundWorker.Defaults;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class Register
	{

		public static BackgroundBuilder AddBackgroundWorkerCore(this IServiceCollection services)
		{
			services.AddSingleton((prov) =>
			{
				return new WorkerProcessor(prov);
			});
			services.AddHostedService<WorkerService>();

			services.AddSingleton(prov =>
			{
				ILogger<Voyager.BackgroundWorker.Guard> logg = prov.GetService<ILogger<Voyager.BackgroundWorker.Guard>>()!;
				if (logg != null)
					return new GuardMonitored(logg);
				else
					return new Voyager.BackgroundWorker.Guard();
			});

			BackgroundBuilder builder = new BackgroundBuilder(services);
			return builder;
		}


		public static BackgroundBuilder AddBackgroundWorker(this IServiceCollection services)
		{
			BackgroundBuilder builder = AddBackgroundWorkerCore(services);
			builder.AddWorker((p) => { return new BlindTask(); });
			builder.AddExternalLock<ExternalLock>();
			builder.SetTimeUpToWakeUp(60);
			return builder;
		}

		public static BackgroundBuilder AddWorker<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this BackgroundBuilder builder)
								where TImplementation : WorkerTask
		{
			builder.Services.AddTransient(typeof(WorkerTask), typeof(TImplementation));
			return builder;
		}


		public static BackgroundBuilder AddWorker(this BackgroundBuilder builder, Func<IServiceProvider, WorkerTask> implementationFactory)
		{
			builder.Services.AddTransient(typeof(WorkerTask), implementationFactory);
			return builder;
		}

		public static BackgroundBuilder AddExternalLock<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this BackgroundBuilder builder)
						where TImplementation : ExternalLock
		{
			builder.Services.AddTransient(typeof(ExternalLock), typeof(TImplementation));
			return builder;
		}


		public static BackgroundBuilder AddExternalLock(this BackgroundBuilder builder, Func<IServiceProvider, ExternalLock> implementationFactory)
		{
			builder.Services.AddTransient(typeof(ExternalLock), implementationFactory);
			return builder;
		}

		public static BackgroundBuilder SetTimeUpToWakeUp(this BackgroundBuilder builder, int timeToWakeUpSec = 60)
		{
			builder.Services.AddTransient((prov) => { return timeToWakeUpSec > -1 ? new TimeToWakeUpSetted(timeToWakeUpSec) : new TimeToWakeUp(); }); ;
			return builder;
		}

	}
}