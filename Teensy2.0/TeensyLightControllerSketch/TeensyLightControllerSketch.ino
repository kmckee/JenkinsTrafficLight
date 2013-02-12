const int redPin = 7;
const int yellowPin = 6;
const int greenPin = 5;
const int fanPin = 3;

const int ON = LOW;
const int OFF = HIGH;

// the setup() method runs once, when the sketch starts
void setup() {
  // initialize the digital pin as an output.
  pinMode(redPin, OUTPUT);
  pinMode(yellowPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  
  
  digitalWrite(redPin, OFF);
  digitalWrite(yellowPin, OFF);
  digitalWrite(greenPin, OFF);
  
  Serial.begin(9600);
}

const int SLEEP_PER_CYCLE = 1000;
const int NUMBER_OF_CYCLES_TO_KEEP_LIGHTS_ON = 60;
int cyclesSinceLastCommandReceived = 0;
int hasCommunicationBeenEstablished = 0;
int isFirstLoop = 1;

void loop() {
  
  
  if (isFirstLoop == 1){
    analogWrite(fanPin, 255);
    warmUpLights();
    isFirstLoop = 0;
  }
  executeAllBufferedCommands();
  delay(SLEEP_PER_CYCLE);
  confirmCommunicationHasNotBeenLost();
}

void warmUpLights() {
  turnOnAllLights();
  delay(1000);
  turnOffAllLights();
}

void confirmCommunicationHasNotBeenLost() {
  if (cyclesSinceLastCommandReceived > NUMBER_OF_CYCLES_TO_KEEP_LIGHTS_ON) {
    turnOffAllLights();
    if (hasCommunicationBeenEstablished == 1) {
      hasCommunicationBeenEstablished = 0;
      warnAboutLossOfCommunication();
    }
  }
  else {
    cyclesSinceLastCommandReceived++;
  }
}

void turnOffAllLights() {
  digitalWrite(redPin, OFF);
  digitalWrite(yellowPin, OFF);
  digitalWrite(greenPin, OFF);
}
void turnOnAllLights() {
  digitalWrite(redPin, ON);
  digitalWrite(yellowPin, ON);
  digitalWrite(greenPin, ON);
}

void warnAboutLossOfCommunication() {
  for (int i=0; i< 5; i++) {
    delay(150);
    digitalWrite(redPin, ON);
    delay(150);
    digitalWrite(yellowPin, ON);
    delay(150);
    digitalWrite(greenPin, ON);
    delay(150);
    digitalWrite(redPin, OFF);
    delay(150);
    digitalWrite(yellowPin, OFF);
    delay(150);
    digitalWrite(greenPin, OFF);
  }
}

void executeAllBufferedCommands()
{
  while(Serial.available()) {
    hasCommunicationBeenEstablished = 1;
    byte incomingByte = Serial.read();
    Serial.println(incomingByte);
    updateLightColor(incomingByte);
    cyclesSinceLastCommandReceived = 0;
  }
}

void updateLightColor(byte command)
{
    if (command == 49) // 1 - red, on
    {
      digitalWrite(redPin, ON);
    }
    else if (command == 50) // 2 - yellow, on
    {
      digitalWrite(yellowPin, ON);
    }
    else if (command == 51) // 3 - green, on
    {
      digitalWrite(greenPin, ON);
    }
    else if (command == 53) // 5 - red, off
    {
      digitalWrite(redPin, OFF);
    }
    else if (command == 54) // 6 - yellow, off
    {
      digitalWrite(yellowPin, OFF);
    }
    else if (command == 55) // 7 - green, on
    {
      digitalWrite(greenPin, OFF);
    }
}

