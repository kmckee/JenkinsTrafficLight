const int redPin = 0;
const int yellowPin = 1;
const int greenPin = 2;

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
  if (Serial.available()) {
    byte incomingByte = Serial.read();
    Serial.println(incomingByte);
    if (incomingByte == 49) // 1 - red, on
    {
      digitalWrite(redPin, ON);
    }
    else if (incomingByte == 50) // 2 - yellow, on
    {
      digitalWrite(yellowPin, ON);
    }
    else if (incomingByte == 51) // 3 - green, on
    {
      digitalWrite(greenPin, ON);
    }
    else if (incomingByte == 53) // 5 - red, off
    {
      digitalWrite(redPin, OFF);
    }
    else if (incomingByte == 54) // 6 - yellow, off
    {
      digitalWrite(yellowPin, OFF);
    }
    else if (incomingByte == 55) // 7 - green, on
    {
      digitalWrite(greenPin, OFF);
    }
  }
}

