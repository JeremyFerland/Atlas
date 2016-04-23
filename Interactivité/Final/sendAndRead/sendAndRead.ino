// Jeremy Ferland inspiration du sketchbook NewPing15Sensors de la librairie NewPing et du sketchbook strandtest de la librairie Adafruit_NeoPixel 

#include <NewPing.h>

const int numberOfDistanceSensor = 12;
const int maxDistanceDistance = 20;  
const int distanceInterval = 40;

unsigned long distanceTimer[numberOfDistanceSensor];
unsigned int distanceStore[numberOfDistanceSensor];
uint8_t currentDistance = 0;

NewPing distanceSensor [numberOfDistanceSensor] = {
  NewPing(30,31,maxDistanceDistance),
  NewPing(32,33,maxDistanceDistance),
  NewPing(34,35,maxDistanceDistance),
  NewPing(36,37,maxDistanceDistance),
  NewPing(38,39,maxDistanceDistance),
  NewPing(40,41,maxDistanceDistance),
  NewPing(42,43,maxDistanceDistance),
  NewPing(44,45,maxDistanceDistance),
  NewPing(46,47,maxDistanceDistance),
  NewPing(48,49,maxDistanceDistance),
  NewPing(50,51,maxDistanceDistance),
  NewPing(52,53,maxDistanceDistance),
};

void setup() {
  // put your setup code here, to run once:
  Serial.begin(57600);
  for (uint8_t i = 1; i < numberOfDistanceSensor; i++){ // Set the starting time for each sensor.
    distanceTimer[i] = distanceTimer[i - 1] + distanceInterval;
  }
}

void loop() {
    for (uint8_t i = 0; i < numberOfDistanceSensor; i++) { // Loop through all the sensors.
    if (millis() >= distanceTimer[i]) {         // Is it this sensor's time to ping?
      distanceTimer[i] += distanceInterval * numberOfDistanceSensor;  // Set next time this sensor will be pinged.
      if (i == 0 && currentDistance == numberOfDistanceSensor - 1) oneSensorCycle(); // Sensor ping cycle complete, do something with the results.
      distanceSensor[currentDistance].timer_stop();          // Make sure previous timer is canceled before starting a new ping (insurance).
      currentDistance = i;                          // Sensor being accessed.
      distanceStore[currentDistance] = 0;                      // Make distance zero in case there's no ping echo for this sensor.
      distanceSensor[currentDistance].ping_timer(echoCheck); // Do the ping (processing continues, interrupt will call echoCheck to look for echo).
    }
  }
}

void echoCheck() { // If ping received, set the sensor distance to array.
  if (distanceSensor[currentDistance].check_timer())
    distanceStore[currentDistance] = distanceSensor[currentDistance].ping_result / US_ROUNDTRIP_CM;
}

void oneSensorCycle() { // Sensor ping cycle complete, do something with the results.
  // The following code would be replaced with your code that does something with the ping results.
  for (uint8_t i = 0; i < numberOfDistanceSensor; i++) {
    Serial.print("/Distance");
    Serial.print(i);
    Serial.print(" ");
    Serial.println(distanceStore[i]);
  }
}

