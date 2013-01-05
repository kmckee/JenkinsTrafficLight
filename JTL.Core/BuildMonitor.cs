using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;


namespace JTL.Core
{
    public interface IBuildMonitor
    {
        BuildStatus CurrentBuildStatus { get; }
    }

    public class BuildMonitor : IBuildMonitor
    {
        private readonly IJenkinsRssFeed _feed;
        public BuildMonitor(IJenkinsRssFeed feed)
        {
            _feed = feed;
            UpdateBuildStatus();
        }

        private void UpdateBuildStatus()
        {
            var xml = HttpUtility.UrlDecode(_feed.Read());

            try
            {
                var doc = XDocument.Load(XmlReader.Create(new StringReader(xml)));

                var latestBuildTitle = (from e in doc.Descendants().Where(e => e.Name.LocalName == "entry")
                                        select e.Elements().First(el => el.Name.LocalName == "title").Value).First();

                if (latestBuildTitle.EndsWith("(aborted)"))
                    CurrentBuildStatus = BuildStatus.Unknown;
                else if (latestBuildTitle.Contains("(broken"))
                    CurrentBuildStatus = BuildStatus.Broken;
                else if (latestBuildTitle.Contains("(stable)") || latestBuildTitle.Contains("(back to normal)"))
                    CurrentBuildStatus = BuildStatus.Successful;
                else if (latestBuildTitle.Contains("fail"))
                    CurrentBuildStatus = BuildStatus.TestFailures;
            }
            catch (XmlException)
            {
                CurrentBuildStatus = BuildStatus.Unknown;
            }
        }

        public BuildStatus CurrentBuildStatus { get; private set; }
    }
}
