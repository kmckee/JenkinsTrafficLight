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
        [TestCase(RssSamples.AbortedBuild, BuildStatus.Unknown, TestName="Status should be unknown when the last build was aborted.")]
        [TestCase(RssSamples.CurrentlyStable, BuildStatus.Stable, TestName = "Status should be Stable when the last build was succesful.")]
        [TestCase(RssSamples.CurrentlyBroken, BuildStatus.Broken, TestName = "Status should be broken when the last build failed.")]
        [TestCase(RssSamples.CurrentlyBrokenPreviouslyYellowThenGreen, BuildStatus.Broken, TestName = "Status should be broken when the last build was failed (alternate example).")]
        [TestCase(RssSamples.BackToNormal, BuildStatus.Stable, TestName = "Status should be Stable when the last build returned to normal.")]
        [TestCase(RssSamples.Stable, BuildStatus.Stable, TestName = "Status should be unknown when the last two builds were successful")]
        [TestCase(RssSamples.OneTestStartedToFail, BuildStatus.TestFailures, TestName = "Status should be TestFailures when the last build introduced a single unit test failure.")]
        [TestCase(RssSamples.MultipleTestsStartedToFail, BuildStatus.TestFailures, TestName = "Status should be TestFailures when the last build introduced a multiple unit test failures.")]
        [TestCase(RssSamples.MoreTestsStartedFailing, BuildStatus.TestFailures, TestName = "Status should be TestFailures when the last build introduced a additional unit test failures.")]
        [TestCase(RssSamples.OneTestStillFailing, BuildStatus.TestFailures, TestName = "Status should be TestFailures when some unit test failures were resolved but one still remains.")]
        [TestCase(RssSamples.MultipleTestsStillFailing, BuildStatus.TestFailures, TestName = "Status should be TestFailures when some unit test failures were resolved but multiple are still failing.")]
        [TestCase("", BuildStatus.Unknown, TestName = "Improperly formatted xml in the rss feed should cause the build status to be unknown")]
        public void CurrentStatus_should_reflect_the_status_of_the_latest_build(string sampleRss, BuildStatus expectedStatus)
        {
            var rssFeedMock = Substitute.For<IJenkinsRssFeed>();
            rssFeedMock.Read().Returns(sampleRss);

            var monitor = new BuildMonitor(rssFeedMock);

            monitor.CurrentBuildStatus.Should().Be(expectedStatus);
        }
    }
}
