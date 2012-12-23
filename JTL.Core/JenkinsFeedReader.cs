using System;
using System.Collections.Generic;
using System.Linq;
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
        public JenkinsRssFeed(string url)
        {
        
        }

        public string Read()
        {
            throw new NotImplementedException();
        }
    }
}
