using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace JTL.Core.Tests
{
    [TestFixture]
    public class JenkinsRssFeedTests
    {
        // This is, unfortunately, dependent on internet connectivity and a remote build server.
        [Test]
        public void Able_to_retrieve_a_feed()
        {
            var feed = new JenkinsRssFeed("https://builds.apache.org/job/ActiveMQ-SysTest-Trunk/rssAll");

            var result = feed.Read();

            result.Should().NotBeNullOrEmpty().And.StartWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n  <feed");
        }
    }
}
