using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JTL.Core
{
    public class BuildMonitor
    {
        public BuildMonitor(IJenkinsRssFeed feed)
        {
        
        }

        public object CurrentBuildStatus
        {
            get { return BuildStatus.Success; }
        }
    }
}
