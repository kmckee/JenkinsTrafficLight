require_relative 'spec_helper'

module JenkinsLight
  describe Monitor do
    let(:output) { double('output').as_null_object }
    let(:traffic_light) { double('traffic_light').as_null_object }
    let(:monitor) { Monitor.new(output, traffic_light) }
   
    def update_monitor status
      VCR.use_cassette(status) do
          monitor.url = "http://fakeurl.com/job/#{status}"
          monitor.update
        end
    end
    
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
      it "outputs a status message based on build status" do
        output.should_receive(:puts).with(/[0-9]+:[0-9]+(:[0-9]+)?\/tBuild Status: Green/)
        update_monitor 'SucceededWithPassingTests' 
      end
      
      it "requests a username if one is needed" do
        output.should_receive(:puts).with("Username:")
        update_monitor 'Authentication'
      end

      it "should not request a username if authentication isn't required" do
        output.should_not_receive(:puts).with("Username:")
        update_monitor 'SucceededWithPassingTests'
      end

      it "requests a password if one is needed" do
        output.should_receive(:puts).with("Password:")
        update_monitor 'Authentication'
      end
      
      it "should get the url" do
        output.stub(:gets).and_return('http://fakeurl.com')
        monitor.start
        monitor.url.should == 'http://fakeurl.com'
      end

      it "updates the traffic light" do
        traffic_light.should_receive(:update).with(:green)
        update_monitor 'SucceededWithPassingTests'
      end
    end

    describe "#request_credentials" do
      it "should get the username" do
        output.stub(:gets).and_return('User', 'Password')
        update_monitor 'Authentication'
        monitor.username.should == 'User'
      end
      
      it "should get the password" do
        output.stub(:gets).and_return('User', 'Password')
        update_monitor 'Authentication'
        monitor.password.should == 'Password'
      end
    end
  end
end
