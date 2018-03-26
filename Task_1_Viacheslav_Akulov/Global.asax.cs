using System.Web.Mvc;
using System.Web.Routing;
using BusinessLogicLayer.Infrastructure;
using Newtonsoft.Json;
using Task1ViacheslavAkulov.Autofac;

namespace Task1ViacheslavAkulov
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AutofacConfig.ConfigureContainer();

			Logger.InitLogger();

			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);

		}
	}
}
