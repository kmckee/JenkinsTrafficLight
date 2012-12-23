using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace JTL.Core.Tests
{
    [TestFixture]
    public class BuildMonitorTests
    {
        [Test]
        public void CurrentStatus_should_be_broken_with_failed_build_in_rss_feed()
        {
            var rssFeedMock = Substitute.For<IJenkinsRssFeed>();
            rssFeedMock.Read().Returns(RssSamples.CurrentlyBroken);

            var monitor = new BuildMonitor(rssFeedMock);

            monitor.CurrentBuildStatus.Should().Be(BuildStatus.Broken);
        }
    }
}
