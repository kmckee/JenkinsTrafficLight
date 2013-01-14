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
      color_details = {
        'blue' => {:status => 'Green', :details => ''},
        'red' => {:status => 'Red', :details => 'Build failed'},
        'disabled' => {:status => 'Unknown', :details => 'Jenkins is suspended'}
      }[get_current_jenkins_color]      
      
      write_status_message(color_details[:status], color_details[:details])
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

    def get_current_jenkins_color
      JSON.parse(RestClient.get(api_url))['color']
    end
  end

end
