using System.Collections.Generic;
using BusinessLogicLayer.Infrastructure;
using System.Web.Mvc;

namespace Task1ViacheslavAkulov.Filters
{
    /// <summary>
    /// Log application exceptions
    /// </summary>
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception != null)
            {
                if (filterContext.Exception is KeyNotFoundException)
                {
                    Logger.Log.Warn($"Warnimg:{ filterContext.Exception.Message }. " +
                                     $"Place { filterContext.Controller}, {filterContext.Exception.TargetSite }");
                    filterContext.ExceptionHandled = true;
                }
                else
                {

                    Logger.Log.Error($"Exception:{ filterContext.Exception.Message }. " +
                                     $"Exception stack trace { filterContext.Exception }");
                    filterContext.ExceptionHandled = true;
                }
            }         
        }
    }
}