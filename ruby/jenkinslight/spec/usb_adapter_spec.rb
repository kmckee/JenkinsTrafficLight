require 'rspec'

module JenkinsLight
  describe UsbAdapter do
    describe "#turn_on" do
      it "writes a '1' to the serial port when passed :red" do
        port = double('port').as_null_object
        subject = UsbAdapter.new port
        
        port.should_receive(:write).with("1")
        
        subject.turn_on :red
      end
      it "writes a '2' to the serial port when passed :yellow"
      it "writes a '3' to the serial port when passed :green"
    end
    describe "#turn_off" do
      it "writes a '5' to the serial port when passed :red" 
      it "writes a '6' to the serial port when passed :yellow"
      it "writes a '7' to the serial port when passed :green"
    end
  end
end
