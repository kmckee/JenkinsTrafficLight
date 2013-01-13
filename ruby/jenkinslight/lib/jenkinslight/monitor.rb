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
      @output.puts(Time.new.strftime("%H:%M:%S") + "/tBuild Status: Green")
    end

    attr_accessor :url
  end

end
