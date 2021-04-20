using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace OMS_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ////// For Authentication of API Calls
            //config.Filters.Add(new APIAuthentication());

            ////// For Exception Handling
            ////// To remove exceptions saving in DB comment the below line 
            ////// Logs will be available in table WebApiErrorLogger
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            config.MessageHandlers.Add(new MessageLoggingHandler());

        }
    }
}
