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
      jenkins_status = get_jenkins_status
      
      status_details = {
        'blue' => {:status => 'Green', :details => ''},
        'red' => {:status => 'Red', :details => 'Build failed'},
        'disabled' => {:status => 'Unknown', :details => 'Jenkins is suspended'},
        'yellow' => {:status => 'Red', :details => 'Failing tests'},
        'red_anime' => {:status => 'Yellow', :details => 'Broken and building...'},
        'error' => {:status => 'Unknown', :details => 'Error contacting Jenkins (404)'},
        'auth' => {:status => 'Unknown', :details => 'Authentication Required (403)'}
      }[jenkins_status]

      request_credentials if  jenkins_status == 'auth'

      write_status_message(status_details[:status], status_details[:details])
    end

    def request_credentials
      @output.puts 'Username:' 
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

    def get_jenkins_status
      begin
        JSON.parse(RestClient.get(api_url))['color']
      rescue RestClient::Forbidden 
        'auth'
      rescue
        'error'
      end
    end
  end

end
