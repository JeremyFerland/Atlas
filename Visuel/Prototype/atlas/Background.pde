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
    padding = 50;
    gridResolution_H = 100;
    gridResolution_V = 100;
    interval_H = width/gridResolution_H;
    interval_V = height/gridResolution_V;
  }


  void display() {

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
  }
}