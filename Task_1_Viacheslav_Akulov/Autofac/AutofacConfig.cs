using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using BusinessLogicLayer.DiModule;
using Task1ViacheslavAkulov.DiModule;

namespace Task1ViacheslavAkulov.Autofac
{
	public class AutofacConfig
	{
		/// <summary>
		/// Autofac config
		/// </summary>
		public static void ConfigureContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			builder.RegisterModule(new ServiceModule("GameStoreConnection"));
			builder.RegisterModule(new WebModule());

			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
