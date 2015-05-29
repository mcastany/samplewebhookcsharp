using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.Configuration;
using System.Security.Cryptography;

namespace SampleSolution.Models
{
    public class SecuredWebhookAttribute : ActionFilterAttribute
    {
        private static string Token = ConfigurationManager.AppSettings["Token"];

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            var content = await actionContext.Request.Content.ReadAsStringAsync();
            IEnumerable<string> githubHashSignature;
            if (!actionContext.Request.Headers.TryGetValues("X-Hub-Signature", out githubHashSignature))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "X-Hub-Signature cannot be empty");
            }

            if (!string.Equals(githubHashSignature.FirstOrDefault(), this.ComputeGithubHashSignature(content)))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hashes don't match");
            }
        }

        private string ComputeGithubHashSignature(string content)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(Token);
            var tokenHmacSha1 = new HMACSHA1(tokenBytes);
            tokenHmacSha1.Initialize();
            var bodyBytes = Encoding.UTF8.GetBytes(content);
            return string.Format("sha1={0}", BitConverter.ToString(tokenHmacSha1.ComputeHash(bodyBytes)).Replace("-", string.Empty).ToLowerInvariant());
        }
    }
}
