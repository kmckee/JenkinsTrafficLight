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
        BuildStatus GetCurrentBuildStatus();
    }

    public class BuildMonitor : IBuildMonitor
    {
        private readonly IJenkinsRssFeed _feed;
        public BuildMonitor(IJenkinsRssFeed feed)
        {
            _feed = feed;
        }

        public BuildStatus GetCurrentBuildStatus()
        {
            var xml = HttpUtility.UrlDecode(_feed.Read());

            try
            {
                var doc = XDocument.Load(XmlReader.Create(new StringReader(xml)));

                var latestBuildTitle = (from e in doc.Descendants().Where(e => e.Name.LocalName == "entry")
                                        select e.Elements().First(el => el.Name.LocalName == "title").Value).First();

                if (latestBuildTitle.EndsWith("(aborted)"))
                    return BuildStatus.Unknown;

                if (latestBuildTitle.Contains("(broken"))
                    return BuildStatus.Broken;

                if (latestBuildTitle.Contains("(stable)") || latestBuildTitle.Contains("(back to normal)"))
                    return BuildStatus.Successful;

                if (latestBuildTitle.Contains("fail"))
                    return BuildStatus.TestFailures;
            }
            catch (XmlException)
            {
                Console.WriteLine("Error parsing response from rss feed.  Check the URL and availability of build server.");
                return BuildStatus.Unknown;
            }
            return BuildStatus.Unknown;
        }

    }
}
