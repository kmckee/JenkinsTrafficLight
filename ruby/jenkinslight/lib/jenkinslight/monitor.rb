require 'json'
require 'rest-client'

module JenkinsLight

  class Monitor
    def initialize output
      @output = output 
    end

    def start
      @output.puts "Jenkins Build Light Monitor Started"
      @output.puts "Enter the URL of the Jenkins job to monitor:"
    end

    def update
      build_details = JSON.parse(RestClient.get(api_url))

      color = build_details['color']

      if color == 'blue'
        @output.puts(Time.new.strftime("%H:%M:%S") + "/tBuild Status: Green")
      elsif color == 'red'
        @output.puts(Time.new.strftime("%H:%M:%S") + "/tBuild Status: Red/tBuild failed")
      end
    end

    attr_accessor :url

    private

    def api_url
      @url + "/api/json"
    end
  end

end
