module JenkinsLight
  class TrafficLight
    def initialize port
      @port = port
    end
    def turn_on_single_light color
      status_map = { 
        :red => '167',
        :yellow => '527',
        :green => '563'
      }  
      @port.write(status_map[color])
    end
  end
end
