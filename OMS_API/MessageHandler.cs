using OMS.DataAccess;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OMS_API
{
    public abstract class MessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);
            var requestMessage = await request.Content.ReadAsByteArrayAsync();
            await IncommingMessageAsync(requestInfo, requestMessage);
            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            await OutgoingMessageAsync(requestInfo, responseMessage);
            return response;
        }

        protected abstract Task IncommingMessageAsync( string requestInfo, byte[] message);
        protected abstract Task OutgoingMessageAsync(string requestInfo, byte[] message);
    }

    public class MessageLoggingHandler : MessageHandler
    {
        protected override async Task IncommingMessageAsync(string requestInfo, byte[] message)
        {
            await Task.Run(() => {
                CommonDataAccess _commonDataAccess = new CommonDataAccess();
                _commonDataAccess.SaveLogs(requestInfo, Encoding.UTF8.GetString(message), "Request");
            });
        }

        protected override async Task OutgoingMessageAsync(string requestInfo, byte[] message)
        {
            await Task.Run(() => {
                CommonDataAccess _commonDataAccess = new CommonDataAccess();
                _commonDataAccess.SaveLogs(requestInfo, Encoding.UTF8.GetString(message), "Response");
            });
        }
    }
}