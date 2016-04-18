#include <Messenger.h>

// Jeremy Ferland inspiration du sketchbook strandtest de la librairie Adafruit_NeoPixel 
#include <Adafruit_NeoPixel.h>

Messenger message = Messenger();
const int numberOfLEDStrip = 5;
const int pinLED[numberOfLEDStrip] = {4,50,51,52,53};
const int pinLEDPassive= 20;
const int nbLED[numberOfLEDStrip] = {240,60,60,60,60};
const int grosseurPorte = 10;

Adafruit_NeoPixel stripPassive = Adafruit_NeoPixel(60, pinLEDPassive, NEO_GRB + NEO_KHZ800);

Adafruit_NeoPixel strip[numberOfLEDStrip] = {
  Adafruit_NeoPixel(nbLED[0], pinLED[0], NEO_GRB + NEO_KHZ800),
  Adafruit_NeoPixel(nbLED[0], pinLED[1], NEO_GRB + NEO_KHZ800),
  Adafruit_NeoPixel(nbLED[0], pinLED[2], NEO_GRB + NEO_KHZ800),
  Adafruit_NeoPixel(nbLED[0], pinLED[3], NEO_GRB + NEO_KHZ800),
  Adafruit_NeoPixel(nbLED[0], pinLED[4], NEO_GRB + NEO_KHZ800)
};
uint32_t color[] = {
  strip[0].Color(0, 0, 0),
  strip[0].Color(5, 0, 25), //le bleu qu'on va vouloir
  strip[0].Color(255, 255, 255),
  strip[0].Color(20, 20, 20),
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
  allColorWipeSameTime(color[1],0,strip[0]);
  allColorWipeSameTime(color[1],0,strip[1]);
  allColorWipeSameTime(color[1],0,strip[2]);
  allColorWipeSameTime(color[1],0,strip[3]);
  allColorWipeSameTime(color[1],0,strip[4]);
  message.attach(messageReceived);
}
int counter;
int opacity = 0;
int indexFlux[] = {-1,-1,-1,-1};

void loop() {
  while ( Serial.available( ) ) message.process(Serial.read( ) );
  /*for (int x = 0; x < 4 ; x++){ // print val of the floor sensor
    Serial.print("Crank");
    Serial.print(x);
    Serial.print(" ");
    Serial.println(analogRead(x));
  }*/
   float temp = millis()/1000.0;
   int opacity = 15 + 15 * sin( temp * 2.0 * PI  );
   for (int x = 0; x < 4 ; x++){
    allColorWipeSameTime(strip[0].Color(opacity,opacity,opacity),0,strip[x+1]);
   }
  for(int x =0; x<4;x++){
    if(indexFlux[x]>=0 && indexFlux[x] <= 60){
      strip[x].setPixelColor(indexFlux[x], strip[0].Color(255,255,255));
      strip[x].setPixelColor(indexFlux[x-1], strip[0].Color(255,255,255));
      strip[x].setPixelColor(indexFlux[x-2], strip[0].Color(255,255,255));
      strip[x].show();
      delay(10);
      indexFlux[x]++;
    }  
  }
}

void messageReceived() { 
  Serial.println("messageReceived");
  if (message.checkString("Door0")) {
    Serial.println("string good");
   openCloseDoor(0,message.readInt());
  }
  else if (message.checkString("Door1")) {
     Serial.println("string good");
   openCloseDoor(1,message.readInt());
  }
  else if (message.checkString("Door2")) {
     Serial.println("string good");
   openCloseDoor(2,message.readInt());
  }
  else if (message.checkString("Door3")) {
     Serial.println("string good");
   openCloseDoor(3,message.readInt());
  }
  else if(message.checkString("Crank0")){
    indexFlux[0] = 0;
  }
  else if(message.checkString("Crank1")){
    indexFlux[1] = 0;
  }
  else if(message.checkString("Crank2")){
    indexFlux[2] = 0;
  }
  else if(message.checkString("Crank3")){
    indexFlux[3] = 0;
  }
  else {
    Serial.println("No string match");
  }
}
void openCloseDoor ( int door, int isOpen){
  Serial.println("test3");
  for(int x =0 ; x < grosseurPorte; x++){
    strip[0].setPixelColor(((nbLED[0]/8)*((2*(door+1))-1))+x,color[isOpen]);
    strip[0].setPixelColor(((nbLED[0]/8)*((2*(door+1))-1))-x,color[isOpen]);  
    strip[0].show();
      Serial.println(((nbLED[0]/8)*((2*(door+1))-1))-x);
    delay(10);
  }
}

void allColorWipeSameTime(uint32_t c, uint8_t wait, Adafruit_NeoPixel currentStrip) {
  for (uint16_t i = 0; i < currentStrip.numPixels(); i++) { // wipe all the strip
    currentStrip.setPixelColor(i, c);
  }
  currentStrip.show();
}

void colorWipe(uint32_t c, uint8_t wait, Adafruit_NeoPixel currentStrip) {
  for (uint16_t i = 0; i < currentStrip.numPixels(); i++) { // wipe all the strip
    currentStrip.setPixelColor(i, c);
    currentStrip.show();
  }
}
