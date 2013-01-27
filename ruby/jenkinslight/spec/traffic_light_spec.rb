require 'rspec'

module JenkinsLight
  describe TrafficLight do
    describe "#turn_on_single_light" do
      let(:port) { double('port').as_null_object }
      subject { TrafficLight.new port } 

      it "writes '167' when passed red" do
        port.should_receive(:write).with("167")
        subject.turn_on_single_light :red
      end
      it "writes '527' when passed yellow" do
        port.should_receive(:write).with("527")
        subject.turn_on_single_light :yellow
      end
      it "writes '563' when passed green" do
        port.should_receive(:write).with("563")
        subject.turn_on_single_light :green
      end
    end
  end
end
