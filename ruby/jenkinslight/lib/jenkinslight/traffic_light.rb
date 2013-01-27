module JenkinsLight
  class TrafficLight
    def initialize port
      @port = port
    end
    def update color
      status_map = { 
        :red => '167',
        :yellow => '527',
        :green => '563'
      }  
      @port.write(status_map[color])
    end

    def turn_off_all_lights
      @port.write('567')
    end
  end
end
