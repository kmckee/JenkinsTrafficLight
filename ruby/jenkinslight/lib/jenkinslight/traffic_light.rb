module JenkinsLight
  class TrafficLight
    def initialize port
      @port = port
    end
    def update color
      status_map = { 
        :red => '167',
        :yellow => '527',
        :green => '563',
        :unknown => '567'
      }  
      @port.write(status_map[color])
    end
  end
end
