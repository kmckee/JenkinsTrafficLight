const int redPin = 7;
const int yellowPin = 6;
const int greenPin = 5;

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


void loop() {

    while(Serial.available()) {
      byte incomingByte = Serial.read();
      Serial.println(incomingByte);
      updateLightColor(incomingByte);
    }
    
    delay(1000);

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

