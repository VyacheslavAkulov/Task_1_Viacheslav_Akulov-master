using System.Web.Mvc;
using Task1ViacheslavAkulov.Filters;

namespace Task1ViacheslavAkulov
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new ActionFilter());
			filters.Add(new ExceptionFilter());
		}
	}
}
