Given /^I am not yet monitoring a build$/ do
end

When /^I start the JenkinsLight monitor$/ do
  monitor = JenkinsLight::Monitor.new(output)
  monitor.start
end

Then /^I should see "(.*?)"$/ do |message|
  output.messages.should include(message)
end

Then /^I should see a message with current time and "(.*?)"$/ do |message|
  regex = Regexp.new(Regexp.new("[0-9]+:[0-9]+(:[0-9]+)?\/t" + message), "i")
  output.messages.to_s.should =~ regex
end

Given /^I am monitoring a build$/ do
  @monitor = JenkinsLight::Monitor.new(output)
  @monitor.start
end

When /^the last build succeeded and all tests passed$/ do
  @monitor.url = "http://fakeurl.com/job/SucceededWithPassingTests"
  @monitor.update
end

When /^the last build failed and a build is not currently in progress$/ do
  @monitor.url = "http://fakeurl.com/job/LastBuildFailed"
  @monitor.update
end


class Output 
  def messages
      @messages ||= []
  end
  def puts(message)
      messages << message
  end 
end

def output
  @output ||= Output.new
end
