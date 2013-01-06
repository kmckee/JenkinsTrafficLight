using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace JTL.Core
{
    public interface IJenkinsRssFeed
    {
        string Read();
    }

    public class JenkinsRssFeed : IJenkinsRssFeed
    {
        private readonly string _url;

        public string UserName { get; set; }
        public string Password { get; set; }

        public JenkinsRssFeed(string url)
        {
            _url = url;
        }

        public string Read()
        {
            try
            {
                var request = CreateHttpRequest();
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                return responseStream != null ? new StreamReader(responseStream).ReadToEnd() : string.Empty;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Message.Contains("403"))
                    throw new AuthorizationFailedException(ex);

                throw;
            }
        }

        private HttpWebRequest CreateHttpRequest()
        {
            var request = (HttpWebRequest)WebRequest.Create(_url);

            if (!string.IsNullOrEmpty(UserName))
            {
                var credentials = String.Format("{0}:{1}", UserName, Password);
                var bytes = Encoding.ASCII.GetBytes(credentials);
                var base64 = Convert.ToBase64String(bytes);
                var authorization = String.Concat("Basic ", base64);
                request.Headers.Add("Authorization", authorization);
            }

            return request;
        }
    }
}
