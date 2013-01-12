require_relative 'spec_helper'

describe JenkinsLight do
  it "must be defined" do
    JenkinsLight::VERSION.should_not be_nil
  end
end
