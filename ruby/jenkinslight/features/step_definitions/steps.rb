Given /^I am not yet monitoring a build$/ do
end

When /^I start the JenkinsLight monitor$/ do
  monitor = JenkinsLight::Monitor.new(output)
  monitor.start
end

Then /^I should see "(.*?)"$/ do |message|
  output.messages.should include(message)
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
