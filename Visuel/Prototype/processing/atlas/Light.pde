class Light {

  PVector loc;

  float fromR, fromG, fromB, toR, toG, toB;
  float r, g, b;
  float lerpSpeed = 0.5;
  float lerpValue;


  Light() {

    r = 243;
    g = 91;
    b = 0;

    loc = new PVector(0, 0, 0);
  }

  public void display() {

    lerpValue += lerpSpeed;

    if (lerpValue >=1) {
      // Get last generated color
      fromR = toR;
      fromG = toG;
      fromB = toB;

      // Create a new destination color
      r = random(20, 50);
      g = random(150, 165);
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

    spotLight(lightR, lightG, lightB, loc.x, loc.y, 1000, 0, 0, -1, PI, 60);
  }
}