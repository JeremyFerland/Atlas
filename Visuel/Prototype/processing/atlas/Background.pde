// Vertex background for light purpose
class Background {

  float gridResolution_H;
  float gridResolution_V;
  float interval_H ;
  float interval_V;
  float padding;

  PImage stone;

  Background() {
    
    stone = loadImage("stone.jpg");
    padding = 0.6;
    gridResolution_H = 100;
    gridResolution_V = 100;
    interval_H = (width/gridResolution_H);
    interval_V = (height/gridResolution_V);
  }


  void display() {

    pushMatrix();
    translate((width-(width*padding))/2,(height-(height*padding))/2);
    scale(1*padding, 1*padding);
    
      //spotLight(39, 163, 163, 0, 0, 2000, 0, 0, -1, PI, 200);
    
    
    // Draw vertex background
    for (int i = 0; i < gridResolution_H; i++) {
      noStroke();
      for (int j = 0; j <gridResolution_V; j++) {
        beginShape();
        texture(stone);
        textureWrap(CLAMP);
        // up left corner
        vertex(i*interval_H, j*interval_V, 0, i*interval_H, j*interval_V);
        // up right corner
        vertex((i+1)*interval_H, j*interval_V, 0, (i+1)*interval_H, j*interval_V);
        // down right corner
        vertex((i+1)*interval_H, (j+1)*interval_V, 0, (i+1)*interval_H, (j+1)*interval_V);
        // down left corner
        vertex(i*interval_H, (j+1)*interval_V, 0, i*interval_H, (j+1)*interval_V);
        endShape(CLOSE);
      }
    }
    popMatrix();
  }
}