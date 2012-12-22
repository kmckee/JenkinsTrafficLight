using System;
using TechTalk.SpecFlow;

namespace JTL.Specs.Features.Steps
{
    [Binding]
    public class BuildStatusSteps
    {
        [Given(@"the current build is successful")]
        public void GivenTheCurrentBuildIsSuccessful()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the current build failed")]
        public void GivenTheCurrentBuildFailed()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the previous build failed")]
        public void GivenThePreviousBuildFailed()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"a build is currently in progress")]
        public void GivenABuildIsCurrentlyInProgress()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the light updates")]
        public void WhenTheLightUpdates()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"an error occurs retrieving the build status")]
        public void WhenAnErrorOccursRetrievingTheBuildStatus()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the ""(.*)"" light should be on")]
        public void ThenTheLightShouldBeOn(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the ""(.*)"" light should be blinking quickly")]
        public void ThenTheLightShouldBeBlinkingQuickly(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
