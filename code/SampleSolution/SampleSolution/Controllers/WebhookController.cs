using SampleSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleSolution.Controllers
{
    [SecuredWebhook]
    public class WebhookController : ApiController
    {
        // POST: api/Webhook
        public async Task<IHttpActionResult> Post()
        {
            return Ok<string>("Payload Received");
        }
    }
}
