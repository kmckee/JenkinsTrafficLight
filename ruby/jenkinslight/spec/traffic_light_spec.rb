require 'rspec'

module JenkinsLight
  describe TrafficLight do
    let(:port) { double('port').as_null_object }
    subject { TrafficLight.new port } 
    describe "#update" do
      it "writes '167' when passed red" do
        port.should_receive(:write).with("167")
        subject.update :red
      end
      it "writes '527' when passed yellow" do
        port.should_receive(:write).with("527")
        subject.update :yellow
      end
      it "writes '563' when passed green" do
        port.should_receive(:write).with("563")
        subject.update :green
      end
      it "writes '567' when passed unknown" do
        port.should_receive(:write).with("567")
        subject.turn_off_all_lights
      end
    end
  end
end
