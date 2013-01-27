module JenkinsLight
  class UsbAdapter
    def initialize port
      @port = port
    end

    def turn_on color
      @port.write("1")
    end
  end
end
