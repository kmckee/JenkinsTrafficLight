require_relative 'spec_helper'

module JenkinsLight
  describe JenkinsFeed do
    let (:feed) { JenkinsFeed.new "http://fakeurl.com" }
    
    describe "#new" do
      it "sets the url" do
        feed.url.should == "http://fakeurl.com"
      end
    end


  end
end
