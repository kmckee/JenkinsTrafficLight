require 'VCR'

RED_ON = "1"
YELLOW_ON = "2"
GREEN_ON = "3"
RED_OFF = "5"
YELLOW_OFF = "6"
GREEN_OFF = "7"

Given /^I am not yet monitoring a build$/ do
end


When /^I start the JenkinsLight monitor$/ do
  start_the_monitor
end

Then /^I should see "(.*?)"$/ do |message|
  output.messages.should include(message)
end

Then /^I should see a message with the current time and "(.*?)"$/ do |message|
  regex = Regexp.new(Regexp.new("[0-9]+:[0-9]+(:[0-9]+)?\/t" + message))
  output.messages.to_s.should =~ regex
end

Given /^I am monitoring a build$/ do
  start_the_monitor
end

When /^the last build succeeded and all tests passed$/ do
  VCR.use_cassette "SucceededWithPassingTests" do
    @monitor.url = "http://fakeurl.com/job/SucceededWithPassingTests"
    @monitor.update
  end
end

When /^the last build failed and a build is not currently in progress$/ do
  VCR.use_cassette "LastBuildFailed" do
    @monitor.url = "http://fakeurl.com/job/LastBuildFailed"
    @monitor.update
  end
end

When /^the jenkins job is disabled$/ do
  VCR.use_cassette "BuildDisabled" do
    @monitor.url = "http://fakeurl.com/job/BuildDisabled"
    @monitor.update
  end
end

When /^the last build succeeded but one or more tests failed$/ do
  VCR.use_cassette "TestFailures" do
    @monitor.url = "http://fakeurl.com/job/TestFailures"
    @monitor.update
  end
end

When /^a build is currently in process and the last build failed$/ do
  VCR.use_cassette "BrokenAndBuilding" do
    @monitor.url = "http://fakeurl.com/job/BrokenAndBuilding"
    @monitor.update
  end
end

When /^it's not possible to contact Jenkins for a status$/ do
  VCR.use_cassette "404NotFound" do
    @monitor.url = "http://fakeurl.com/job/404NotFound"
    @monitor.update
  end
end

When /^I monitor a build that requires basic authentication$/ do
  start_the_monitor 

  VCR.use_cassette "Authentication" do
    @monitor.url = "http://fakeurl.com/job/Authentication"
    @monitor.update
  end
end

Then /^I should be prompted to enter a user name$/ do
  @output.messages.should include("Username:")
end
  
Then /^I should be prompted to enter a password$/ do
  @output.messages.should include("Password:")
end

When /^a build is currently in process and the last build had test failures$/ do
  VCR.use_cassette "TestsFailedAndBuilding" do
    @monitor.url = "http://fakeurl.com/job/TestsFailedAndBuilding"
    @monitor.update
  end
end

Given /^the build is currently Green$/ do
  start_the_monitor
  VCR.use_cassette "SucceededWithPassingTests" do
    @monitor.url = "http://fakeurl.com/job/SucceededWithPassingTests"
    @monitor.update
  end
end

When /^I look at the traffic light$/ do

end

Then /^only the green light should be on$/ do
  pending 'refactor'
  usb.messages.should include("5")
  usb.messages.should include("6")
  usb.messages.should include("3")
end

def start_the_monitor
  @monitor = JenkinsLight::Monitor.new(output)
  @monitor.start
end

def output
  @output ||= Output.new
end

class Output 
  
  def messages
      @messages ||= []
  end
  def puts(message)
      messages << message
  end 

  def gets
    ''
  end
end

def usb
  @usb ||= Usb.new
end

class Usb
  def messages
    @messages ||= []
  end
end
