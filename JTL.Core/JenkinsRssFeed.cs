using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public JenkinsRssFeed(string url)
        {
            _url = url;
        }

        public string Read()
        {
            var request = WebRequest.Create(_url);

            var responseStream = request.GetResponse().GetResponseStream();

            return responseStream != null ? new StreamReader(responseStream).ReadToEnd() : string.Empty;
        }
    }
}
