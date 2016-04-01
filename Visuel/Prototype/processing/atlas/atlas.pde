/* 
 
 Created by Marc-Olivier Héroux-Hould ©2016 V1.0
 Created on March 17, 2016
 Processing V3.0.2
 
 Description : 
 Developer notes : 
 Licence : This work is licensed under the Creative Commons Attribution-NonCommercial 4.0 International License.
 
 Title : 
 
 */

// Syphon library
import codeanticode.syphon.*;
SyphonServer syphonServer;

Background background;

Pyramid pyramid;

PathFinder pathFinder;

Symbols symbols;

void settings() {
  // 2 FullHD projectors
  size(1920, 1920, P3D);
  PJOGL.profile=1;
}

void setup() {
  background(0);

  background = new Background();
  
  pyramid = new Pyramid();

  pathFinder = new PathFinder();
  
  symbols = new Symbols();

  // Create syhpon server to send frames out.
  syphonServer = new SyphonServer(this, "Processing Syphon");
}

void draw() {
  background(0);
  
  background.display();
  
  pyramid.display();
  
  pathFinder.display();

  symbols.display();

  // ** LEAVE AT THE END OF DRAW FUNCTION **
  syphonServer.sendScreen();
}