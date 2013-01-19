require 'json'
require 'rest-client'

module JenkinsLight

  class Monitor
    attr_accessor :url, :username, :password
    
    def initialize output
      @output = output 
    end

    def start
      @output.puts "Jenkins Build Light Monitor Started"
      @output.puts "Enter the URL of the Jenkins job to monitor:"
    end

    def update
      status_details = get_jenkins_status
      
      request_credentials if status_details[:code] == :auth

      write_status_message(status_details[:status], status_details[:details])
    end
    
    def request_credentials
      @output.puts 'Username:' 
      @username = @output.gets

      @output.puts 'Password:'
      @password = @output.gets
    end

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
        status = JSON.parse(RestClient.get(api_url))['color']
      rescue RestClient::Forbidden 
        status = 'auth'
      rescue
        status = 'error'
      end

      { 'blue' => {:status => 'Green', :details => ''},
        'red' => {:status => 'Red', :details => 'Build failed'},
        'disabled' => {:status => 'Unknown', :details => 'Jenkins is suspended'},
        'yellow' => {:status => 'Red', :details => 'Failing tests'},
        'red_anime' => {:status => 'Yellow', :details => 'Broken and building...'},
        'error' => {:status => 'Unknown', :details => 'Error contacting Jenkins (404)'},
        'auth' => {:status => 'Unknown', :details => 'Authentication Required (403)', :code => :auth}
      }[status]
    end
  end

end
