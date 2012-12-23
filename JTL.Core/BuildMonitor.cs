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
            CurrentBuildStatus = feed.Read().Contains("ActiveMQ Protocol Buffer") ? BuildStatus.Broken : BuildStatus.Success;
        }

        public BuildStatus CurrentBuildStatus { get; private set; }
    }
}
