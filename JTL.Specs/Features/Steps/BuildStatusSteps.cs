﻿using System;
using FluentAssertions;
using JTL.Core;
using NSubstitute;
using TechTalk.SpecFlow;

namespace JTL.Specs.Features.Steps
{
    [Binding]
    public class BuildStatusSteps
    {
        private const string RssFeedGreenCurrentRedPrevious = @"<?xml version=""1.0"" encoding=""UTF-8""?><feed xmlns=""http://www.w3.org/2005/Atom""><title>Accumulo-1.3.x all builds</title><link type=""text/html"" href=""https://builds.apache.org/job/Accumulo-1.3.x/"" rel=""alternate""/><updated>2012-12-18T15:38:53Z</updated><author><name>Jenkins Server</name></author><id>urn:uuid:903deee0-7bfa-11db-9fe1-0800200c9a66</id><entry><title>Accumulo-1.3.x #57 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/Accumulo-1.3.x/57/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2012:Accumulo-1.3.x:2012-12-18_15-38-53</id><published>2012-12-18T15:38:53Z</published><updated>2012-12-18T15:38:53Z</updated></entry><entry><title>Accumulo-1.3.x #56 (back to normal)</title><link type=""text/html"" href=""https://builds.apache.org/job/Accumulo-1.3.x/56/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2012:Accumulo-1.3.x:2012-07-11_00-10-41</id><published>2012-07-11T00:10:41Z</published><updated>2012-07-11T00:10:41Z</updated></entry><entry><title>Accumulo-1.3.x #55 (broken since build #54)</title><link type=""text/html"" href=""https://builds.apache.org/job/Accumulo-1.3.x/55/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2012:Accumulo-1.3.x:2012-07-08_00-12-45</id><published>2012-07-08T00:12:45Z</published><updated>2012-07-08T00:12:45Z</updated></entry><entry><title>Accumulo-1.3.x #54 (broken since this build)</title><link type=""text/html"" href=""https://builds.apache.org/job/Accumulo-1.3.x/54/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2012:Accumulo-1.3.x:2012-07-07_00-07-29</id><published>2012-07-07T00:07:29Z</published><updated>2012-07-07T00:07:29Z</updated></entry><entry><title>Accumulo-1.3.x #53 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/Accumulo-1.3.x/53/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2012:Accumulo-1.3.x:2012-07-03_09-42-22</id><published>2012-07-03T09:42:22Z</published><updated>2012-07-03T09:42:22Z</updated></entry></feed>";
        //private const string RssFeedRedCurrentGreenPrevious = "<?xml version=""1.0"" encoding=""UTF-8""?><feed xmlns=""http://www.w3.org/2005/Atom""><title>ActiveMQ Protocol Buffer all builds</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/"" rel=""alternate""/><updated>2011-01-06T19:08:58Z</updated><author><name>Jenkins Server</name></author><id>urn:uuid:903deee0-7bfa-11db-9fe1-0800200c9a66</id><entry><title>ActiveMQ Protocol Buffer #12 (broken since this build)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/12/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2011:ActiveMQ Protocol Buffer:2011-01-06_19-08-58</id><published>2011-01-06T19:08:58Z</published><updated>2011-01-06T19:08:58Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #11 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/11/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2010:ActiveMQ Protocol Buffer:2010-08-03_15-01-46</id><published>2010-08-03T15:01:46Z</published><updated>2010-08-03T15:01:46Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #10 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/10/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2010:ActiveMQ Protocol Buffer:2010-07-14_07-12-57</id><published>2010-07-14T07:12:57Z</published><updated>2010-07-14T07:12:57Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #9 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/9/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2010:ActiveMQ Protocol Buffer:2010-05-24_03-00-37</id><published>2010-05-24T03:00:37Z</published><updated>2010-05-24T03:00:37Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #8 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/8/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2010:ActiveMQ Protocol Buffer:2010-04-23_16-11-38</id><published>2010-04-23T16:11:38Z</published><updated>2010-04-23T16:11:38Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #7 (back to normal)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/7/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2009:ActiveMQ Protocol Buffer:2009-09-21_07-49-50</id><published>2009-09-21T07:49:50Z</published><updated>2009-09-21T07:49:50Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #6 (broken since build #5)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/6/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2009:ActiveMQ Protocol Buffer:2009-09-20_15-01-48</id><published>2009-09-20T15:01:48Z</published><updated>2009-09-20T15:01:48Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #5 (broken since this build)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/5/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2009:ActiveMQ Protocol Buffer:2009-09-19_15-01-50</id><published>2009-09-19T15:01:50Z</published><updated>2009-09-19T15:01:50Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #4 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/4/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2009:ActiveMQ Protocol Buffer:2009-09-17_21-19-40</id><published>2009-09-17T21:19:40Z</published><updated>2009-09-17T21:19:40Z</updated></entry><entry><title>ActiveMQ Protocol Buffer #3 (stable)</title><link type=""text/html"" href=""https://builds.apache.org/job/ActiveMQ%20Protocol%20Buffer/3/"" rel=""alternate""/><id>tag:hudson.dev.java.net,2009:ActiveMQ Protocol Buffer:2009-06-23_12-36-25</id><published>2009-06-23T12:36:25Z</published><updated>2009-06-23T12:36:25Z</updated></entry></feed>"

        private BuildMonitor _buildMonitor;

        [When(@"the current build is successful")]
        public void WhenTheCurrentBuildIsSuccessful()
        {
            var mockFeedReader = Substitute.For<IJenkinsRssFeed>();
            mockFeedReader.Read().Returns(RssFeedGreenCurrentRedPrevious);
            
            _buildMonitor = new BuildMonitor(mockFeedReader);
        }

        [Then(@"the ""(.*)"" light should be on")]
        public void ThenTheLightShouldBeOn(string lightColor)
        {
            var expectedStatus = ConvertColorToBuildStatus(lightColor);

            _buildMonitor.CurrentBuildStatus.Should().Be(expectedStatus);
        }


        private static BuildStatus ConvertColorToBuildStatus(string lightColor)
        {
            BuildStatus expectedStatus;
            switch (lightColor.ToLower())
            {
                case "green":
                    expectedStatus = BuildStatus.Success;
                    break;
                case "yellow":
                    expectedStatus = BuildStatus.BrokenAndBuilding;
                    break;
                default:
                    expectedStatus = BuildStatus.Broken;
                    break;
            }
            return expectedStatus;
        }
    }
}
