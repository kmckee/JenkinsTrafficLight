require_relative 'spec_helper'

module JenkinsLight
  describe Monitor do
    
    describe "#new" do
      let(:output) { double('output').as_null_object }
      let(:monitor) { Monitor.new(output) }
      it "should output a startup message" do 
        output.should_receive(:puts).with("Jenkins Build Light Monitor Started")
        monitor.start
      end
      it "should request the user enter the url of a Jenkins job" do
        output.should_receive(:puts).with("Enter the URL of the Jenkins job to monitor:")
        monitor.start
      end
    end

  end
end
