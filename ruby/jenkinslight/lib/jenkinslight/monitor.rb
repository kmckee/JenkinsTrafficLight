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
        write_status_message('Green', '')
      elsif color == 'red'
        write_status_message('Red', 'Build failed')
      elsif color == 'disabled'
        write_status_message('Unknown', 'Jenkins is suspended')
      end
    end

    attr_accessor :url

    private

    def api_url
      @url + "/api/json"
    end

    def write_status_message(status, details)
        time = Time.new.strftime("%H:%M:%S") 
        @output.puts("#{time}/tBuild Status: #{status}/t#{details}")
    end

  end

end
