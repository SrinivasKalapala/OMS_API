using OMS.DataAccess;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace OMS_API
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            var httpException = exception as HttpException;
            if (httpException != null)
            {
                context.Result = new CustomErrorResult(context.Request,
                    (HttpStatusCode)httpException.GetHttpCode(),
                     httpException.Message);
                return;
            }

            // Return HttpStatusCode for other types of exception.
            context.Result = new CustomErrorResult(context.Request,
                HttpStatusCode.InternalServerError,
                exception.Message);
        }
    }

    public class CustomErrorResult : System.Web.Http.IHttpActionResult
    {
        private readonly string _errorMessage;
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;

        public CustomErrorResult(HttpRequestMessage requestMessage,
           HttpStatusCode statusCode, string errorMessage)
        {
            _requestMessage = requestMessage;
            _statusCode = statusCode;
            _errorMessage = errorMessage;

            CommonDataAccess _commonDataAccess = new CommonDataAccess();
            _commonDataAccess.SaveExceptions(Convert.ToString(requestMessage.RequestUri), errorMessage);
            _commonDataAccess.CloseConnection();
        }

        public Task<HttpResponseMessage> ExecuteAsync(
           CancellationToken cancellationToken)
        {
            return Task.FromResult(_requestMessage.CreateErrorResponse(
                _statusCode, _errorMessage));
        }
    }
}