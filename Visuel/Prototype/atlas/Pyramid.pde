class Pyramid {

  PVector loc;
  float pyramidSize = 200;

  PImage texture;

  float fromR, fromG, fromB, toR, toG, toB;
  float r, g, b;
  float lerpSpeed = 0.5;
  float lerpValue;

  Pyramid() {

    r = 243;
    g = 91;
    b = 0;

    loc = new PVector(0, 0, 0);
    loc.x = width/2;
    loc.y = height/2;

    texture = loadImage("sand.jpg");
  }

  void display() {

    light();

    // Shape #1
    beginShape();
    texture(texture);
    textureWrap(REPEAT); 
    vertex(loc.x, loc.y, loc.x, loc.y);
    vertex(loc.x+pyramidSize, loc.y+pyramidSize, loc.x+pyramidSize, loc.y+pyramidSize);
    vertex(loc.x-pyramidSize, loc.y+pyramidSize, loc.x-pyramidSize, loc.y+pyramidSize);
    endShape(CLOSE);

    // Shape #2
    beginShape();
    texture(texture);
    textureWrap(REPEAT); 
    vertex(loc.x, loc.y, loc.x, loc.y);
    vertex(loc.x-pyramidSize, loc.y+pyramidSize, loc.x-pyramidSize, loc.y+pyramidSize);
    vertex(loc.x-pyramidSize, loc.y-pyramidSize, loc.x-pyramidSize, loc.y-pyramidSize);
    endShape(CLOSE);

    // Shape #3
    beginShape();
    texture(texture);
    textureWrap(REPEAT); 
    vertex(loc.x, loc.y, loc.x, loc.y);
    vertex(loc.x+pyramidSize, loc.y+pyramidSize, loc.x+pyramidSize, loc.y+pyramidSize);
    vertex(loc.x+pyramidSize, loc.y-pyramidSize, loc.x+pyramidSize, loc.y-pyramidSize);
    endShape(CLOSE);

    // Shape #4
    beginShape();
    texture(texture);
    textureWrap(REPEAT); 
    vertex(loc.x, loc.y, loc.x, loc.y);
    vertex(loc.x-pyramidSize, loc.y-pyramidSize, loc.x-pyramidSize, loc.y-pyramidSize);
    vertex(loc.x+pyramidSize, loc.y-pyramidSize, loc.x+pyramidSize, loc.y-pyramidSize);
    endShape(CLOSE);
  }

  void light() {

    lerpValue += lerpSpeed;

    if (lerpValue >=1) {
      // Get last generated color
      fromR = toR;
      fromG = toG;
      fromB = toB;

      // Create a new destination color
      r = random(20, 50);
      g = random(150, 175);
      b = random(150, 175);

      //Set color destination value
      toR = r;
      toG = g;
      toB = b;

      // Reset
      lerpValue = 0;
    }

    // Lerp between color thru time
    float lightR = lerp(fromR, toR, lerpValue);
    float lightG = lerp(fromG, toG, lerpValue);
    float lightB = lerp(fromB, toB, lerpValue);

    spotLight(lightR, lightG, lightB, loc.x, loc.y, 1000, 0, 0, -1, PI, 200);
  }
}