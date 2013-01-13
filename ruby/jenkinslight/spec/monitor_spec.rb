require_relative 'spec_helper'

module JenkinsLight
  describe Monitor do
    
    it "should output a startup message" do 
      output = double('output').as_null_object
      monitor = Monitor.new(output)

      output.should_receive(:puts).with("Jenkins Build Light Monitor Started")
      
      monitor.start
    end
    it "should request the user enter the url of a Jenkins job" do
      output = double('output').as_null_object
      monitor = Monitor.new(output)

      output.should_receive(:puts).with("Enter the URL of the Jenkins job to monitor:")
      
      monitor.start
    end
  end
end
