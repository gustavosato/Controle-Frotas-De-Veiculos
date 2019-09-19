using Lean.Test.Cloud.WebApi.Common;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lean.Test.Cloud.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private HttpResponseMessage _responseMessage;
        public BaseController()
        {
            _responseMessage = new HttpResponseMessage();
        }

        protected Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result)
        {
            _responseMessage = Request.CreateResponse(code, result);

            return Task.FromResult<HttpResponseMessage>(_responseMessage);
        }

        protected Task<HttpResponseMessage> CreateResponseText(HttpStatusCode code, object result)
        {
            _responseMessage = Request.CreateResponse(code);
            _responseMessage.Content = new StringContent(result.ToString(), Encoding.UTF8, MediaTypeConst.TextPlain);

            return Task.FromResult<HttpResponseMessage>(_responseMessage);
        }
    }
}