module JenkinsLight
  class TrafficLight
    def initialize port
      @port = port
    end
    def turn_on_single_light color
      @port.write('167')
    end
  end
end
