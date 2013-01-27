require_relative 'spec_helper'

module JenkinsLight
  describe JenkinsFeed do
    
    describe "#new" do
      it "sets the url" do
        feed = JenkinsFeed.new "http://fakeurl.com" 
        feed.url.should == "http://fakeurl.com"
      end
    end

    describe "#get_status" do
      it "outputs 'Green' if all tests pass" do
        get_status('SucceededWithPassingTests').should == :green
      end
      it "outputs 'Red' if the build failed" do
        get_status('LastBuildFailed').should == :red
      end
      it "outputs 'Unknown' if the build job is disabled" do
        get_status('BuildDisabled').should == :unknown
      end
      it "outputs 'Red' if there are failing tests" do
        get_status('TestFailures').should == :red
      end
      it "outputs 'Yellow' if the build is broken and building" do
        get_status('BrokenAndBuilding').should == :yellow
      end
      it "outputs 'Yellow' if the tests failed previously and a build is in progress" do
        get_status('TestsFailedAndBuilding').should == :yellow
      end
      it "outputs 'Unknown' if there is an error contacting jenkins" do
        get_status('404NotFound').should == :unknown
      end
      it "outputs 'Unknown' if the server requires authentication" do
        get_status('Authentication').should == :unknown
      end
    end

    def get_status cassette_name
      VCR.use_cassette(cassette_name) do
        feed = JenkinsFeed.new "http://fakeurl.com/job/#{cassette_name}"
        feed.get_status[:status]
      end
    end

  end
end
