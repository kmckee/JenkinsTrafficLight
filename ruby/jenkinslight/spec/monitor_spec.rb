require_relative 'spec_helper'

module JenkinsLight
  describe Monitor do
    let(:output) { double('output').as_null_object }
    let(:monitor) { Monitor.new(output) }
    
    describe "#new" do
      it "should output a startup message" do 
        output.should_receive(:puts).with("Jenkins Build Light Monitor Started")
        monitor.start
      end
      it "should request the user enter the url of a Jenkins job" do
        output.should_receive(:puts).with("Enter the URL of the Jenkins job to monitor:")
        monitor.start
      end
    end

    describe "#url" do
      it "should accept a url" do
        monitor.url = "http://www.google.com"
        monitor.url.should == "http://www.google.com"
      end
    end

    describe "#update" do
      it "should output a status message if the last build was successful with passing tests" do
        output.should_receive(:puts).with(/[0-9]+:[0-9]+(:[0-9]+)?\/tBuild Status: Green/)
        
        VCR.use_cassette('SucceededWithPassingTests') do
          monitor.url = "http://fakeurl.com/job/SucceededWithPassingTests"
          monitor.update
        end
      end
      
      it "should output a status message if the last build failed" do
        output.should_receive(:puts).with(/[0-9]+:[0-9]+(:[0-9]+)?\/tBuild Status: Red(.*)/)
        
        VCR.use_cassette('LastBuildFailed') do
          monitor.url = "http://fakeurl.com/job/LastBuildFailed"
          monitor.update
        end
      end

    end
  end
end
