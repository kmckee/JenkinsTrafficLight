module JenkinsLight

  class Monitor
    attr_accessor :url, :username, :password
    
    def initialize output, traffic_light
      @output = output
      @traffic_light = traffic_light
    end

    def start
      @output.puts "Jenkins Build Light Monitor Started"
      @output.puts "Enter the URL of the Jenkins job to monitor:"
      @url = @output.gets
    end

    def update
      status_details = jenkins_feed.get_status
      request_credentials if status_details[:code] == :auth
      
      write_status_message(status_details[:status], status_details[:details])
      @traffic_light.update(status_details[:status])      
    end
    
    def request_credentials
      @output.puts 'Username:' 
      @username = @output.gets

      @output.puts 'Password:'
      @password = @output.gets
    end

    private

    def write_status_message(status, details)
      time = Time.new.strftime("%H:%M:%S") 
      @output.puts("#{time}/tBuild Status: #{status.capitalize}/t#{details}")
    end

    def jenkins_feed
      @jenkins_feed ||= JenkinsFeed.new @url
    end
  end
end
