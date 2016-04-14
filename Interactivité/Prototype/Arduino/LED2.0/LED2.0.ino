// Jeremy Ferland inspiration du sketchbook strandtest de la librairie Adafruit_NeoPixel 

#include <Messenger.h>
#include <Adafruit_NeoPixel.h>

Messenger message = Messenger();
const int numberOfLEDStrip = 1;
const int pinLED[numberOfLEDStrip] = {4};
const int pinLEDPassive= 20;
const int nbLED[numberOfLEDStrip] = {300};
const int grosseurPorte = 10;

Adafruit_NeoPixel stripPassive = Adafruit_NeoPixel(60, pinLEDPassive, NEO_GRB + NEO_KHZ800);

Adafruit_NeoPixel strip[numberOfLEDStrip] = {
  Adafruit_NeoPixel(nbLED[0], pinLED[0], NEO_GRB + NEO_KHZ800)
};
uint32_t color[] = {
  strip[0].Color(0, 0, 0),
  strip[0].Color(0, 0, 255), //le bleu qu'on va vouloir
  strip[0].Color(255, 0, 0),
  strip[0].Color(0, 255, 0),
};
boolean empty = true;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(57600);
  // put all LED to off
  for ( int i = 0 ; i < numberOfLEDStrip ; i++) {
    strip[i].begin();
    strip[i].show();
  }
  stripPassive.begin();
  stripPassive.show();
  colorWipe(color[2], 0, stripPassive);
  
  message.attach(messageReceived);
}
int counter;
void loop() {
  if ( Serial.available( ) > 0 ) {
    message.process( Serial.read( ) );
  }
}

void messageReceived() { 
  if (message.checkString("Door0")) {
   openCloseDoor(0,message.readInt());
  }
  if (message.checkString("Door1")) {
   openCloseDoor(1,message.readInt());
  }
  if (message.checkString("Door2")) {
   openCloseDoor(2,message.readInt());
  }
  if (message.checkString("Door3")) {
   openCloseDoor(3,message.readInt());
  }
}
void openCloseDoor ( int door, int isOpen){
  for(int x =0 ; x < grosseurPorte; x++){
    strip[0].setPixelColor(((nbLED[0]/8)*((2*(door+1))-1))+x,color[isOpen]);
    strip[0].setPixelColor(((nbLED[0]/8)*((2*(door+1))-1))-x,color[isOpen]);  
  }
}

void colorWipe(uint32_t c, uint8_t wait, Adafruit_NeoPixel currentStrip) {
  for (uint16_t i = 0; i < currentStrip.numPixels(); i++) { // wipe all the strip
    currentStrip.setPixelColor(i, c);
    currentStrip.show();
  }
}
