using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Skype.Bots;
using Microsoft.Skype.Bots.Filters;
using Microsoft.Skype.Bots.Interfaces;

namespace EchoBotQuickStart.Controllers
{
    public class EchoBotController : BotController
    {
        #region Static Fields

        private static readonly MtlsAuthenticationFromHeader MtlsAuth = new MtlsAuthenticationFromHeader();

        #endregion

        #region Constructors and Destructors

        public EchoBotController(
            IMessagingBotService messageProcessor) : base(messageProcessor, "EchoBot")
        {
        }

        #endregion

        #region Public Methods and Operators

        [Route("v1/echo")]
        public override Task<HttpResponseMessage> ProcessMessagingEventAsync()
        {
            /* Uncomment following lines to enable https verification for Azure.

            if (!MtlsAuth.ValidateClientCertificate(Request.RequestUri.ToString(), Request.Headers, GetClientIp()))
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Forbidden));
            }
            */

            return base.ProcessMessagingEventAsync();
        }

        #endregion

        #region Methods

        private string GetClientIp()
        {
            const string MsHttpContextName = "MS_HttpContext";

            if (Request.Properties.ContainsKey(MsHttpContextName))
            {
                return ((HttpContextWrapper) Request.Properties[MsHttpContextName]).Request.UserHostAddress;
            }

            return null;
        }

        #endregion
    }
}