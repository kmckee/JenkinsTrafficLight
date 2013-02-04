require 'VCR'
require 'rspec'

Given /^I am not yet monitoring a build$/ do
end

When /^I start the JenkinsLight monitor$/ do
  start_the_monitor
end

Then /^I should see "(.*?)"$/ do |message|
  output.messages.should include(message)
end

Then /^I should see a message with the current time and "(.*?)"$/ do |message|
  regex = Regexp.new(Regexp.new("[0-9]+:[0-9]+(:[0-9]+)?.*" +  message))
  output.messages.to_s.should =~ regex
end

Given /^I am monitoring a build$/ do
  start_the_monitor
end

When /^the last build succeeded and all tests passed$/ do
  update_monitor_using "SucceededWithPassingTests" 
end

When /^the last build failed and a build is not currently in progress$/ do
  update_monitor_using "LastBuildFailed"
end

When /^the jenkins job is disabled$/ do
  update_monitor_using "BuildDisabled" 
end

When /^the last build succeeded but one or more tests failed$/ do
  update_monitor_using "TestFailures"
end

When /^a build is currently in process and the last build failed$/ do
  update_monitor_using "BrokenAndBuilding" 
end

When /^it's not possible to contact Jenkins for a status$/ do
  update_monitor_using "404NotFound"
end

When /^I monitor a build that requires basic authentication$/ do
  start_the_monitor 
  update_monitor_using "Authentication" 
end

Then /^I should be prompted to enter a user name$/ do
  @output.messages.should include("Username:")
end
  
Then /^I should be prompted to enter a password$/ do
  @output.messages.should include("Password:")
end

When /^a build is currently in process and the last build had test failures$/ do
  update_monitor_using "TestsFailedAndBuilding" 
end

Given /^the build is currently Green$/ do
  start_the_monitor
  update_monitor_using "SucceededWithPassingTests"
end

Given /^the build is currently Red$/ do
  start_the_monitor
  update_monitor_using "LastBuildFailed"
end

When /^I look at the traffic light$/ do

end

Then /^only the (.*) light should be on$/ do |color|
  traffic_light.colors_turned_on.should include(color.downcase.to_sym)
end

Given /^the previous build was unhealthy and a build is in progress$/ do
  start_the_monitor
  update_monitor_using "BrokenAndBuilding"
end


Given /^the build status is unknown$/ do
  start_the_monitor
  update_monitor_using "404NotFound"
end

Then /^all lights should be off$/ do
  traffic_light.colors_turned_on.should include(:unknown)
end

def start_the_monitor
  @monitor = JenkinsLight::Monitor.new(output, input, traffic_light)
  @monitor.start
end

def update_monitor_using cassette_name
  VCR.use_cassette cassette_name do
    @monitor.url = "http://fakeurl.com/job/#{cassette_name}"
    @monitor.update
  end
end

def traffic_light
  @traffic_light ||= Light.new 
end

class Light
  def colors_turned_on
    @colors_turned_on ||= [] 
  end
  def update color
    colors_turned_on << color
  end
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
end

def input
  @input ||= Input.new
end

class Input
  def readline
    ''
  end
end
