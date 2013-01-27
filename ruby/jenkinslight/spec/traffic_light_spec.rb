require 'rspec'

module JenkinsLight
  describe TrafficLight do
    describe "#turn_on_single_light" do
      let(:port) { double('port').as_null_object }
      subject { TrafficLight.new port } 

      it "writes '167' when passed red" do
        port.should_receive(:write).with("167")
        subject.turn_on_single_light "Red"
      end
      it "writes '527' when passed yellow"
      it "writes '563' when passed green"
    end
  end
end
