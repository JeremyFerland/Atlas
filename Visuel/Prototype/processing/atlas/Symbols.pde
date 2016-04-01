class Symbols {

  // Circle position
  PVector s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12;
  float padding;

  float theta;
  int circleResolution;
  float diam;

  PImage sand;

  Light[] lights = new Light[12];

  Symbols() {

    // Circle position
    s1 = new PVector();
    s2 = new PVector();
    s3 = new PVector();
    s4 = new PVector();
    s5 = new PVector();
    s6 = new PVector();
    s7 = new PVector();
    s8 = new PVector();
    s9 = new PVector();
    s10 = new PVector();
    s11 = new PVector();
    s12 = new PVector();

    padding = 100;

    // Top position
    s1.x = 1*(width/3)-padding;
    s2.x = 1.5*(width/3);
    s3.x = 2*(width/3)+padding;

    s1.y = (abs((background.padding*height)-height)/2)/2;
    s2.y = (abs((background.padding*height)-height)/2)/2;
    s3.y = (abs((background.padding*height)-height)/2)/2;

    // Right position
    s4.x = width-(abs((background.padding*width)-width)/2)/2;
    s5.x = width-(abs((background.padding*width)-width)/2)/2;
    s6.x = width-(abs((background.padding*width)-width)/2)/2;

    s4.y = 1*(height/3)-padding;
    s5.y = 1.5*(height/3);
    s6.y = 2*(height/3)+padding;

    // Bottom position
    s7.x = 1*(width/3)-padding;
    s8.x = 1.5*(width/3);
    s9.x = 2*(width/3)+padding;

    s7.y = height-(abs((background.padding*height)-height)/2)/2;
    s8.y = height-(abs((background.padding*height)-height)/2)/2;
    s9.y = height-(abs((background.padding*height)-height)/2)/2;

    // Left position
    s10.x = (abs((background.padding*width)-width)/2)/2;
    s11.x = (abs((background.padding*width)-width)/2)/2;
    s12.x = (abs((background.padding*width)-width)/2)/2;

    s10.y = 1*(height/3)-padding;
    s11.y = 1.5*(height/3);
    s12.y = 2*(height/3)+padding;

    // Vertex circle settings
    diam = 120;
    circleResolution = 80;
    theta = TWO_PI / circleResolution;

    sand = loadImage("sand.jpg");

    for (int i = 0; i <12; i++) {
      lights[i] = new Light();
    }
  }

  void display() {

    // 1st symbol
    pushMatrix();
    translate(s1.x, s1.y);
    beginShape();
    lights[0].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 2nd symbol
    pushMatrix();
    translate(s2.x, s2.y);
    beginShape();
    lights[1].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 3rd symbol
    pushMatrix();
    translate(s3.x, s3.y);
    lights[2].display();
    beginShape();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 4 symbol
    pushMatrix();
    translate(s4.x, s4.y);
    beginShape();
    lights[3].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 5 symbol
    pushMatrix();
    translate(s5.x, s5.y);
    beginShape();
    lights[4].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 6 symbol
    pushMatrix();
    translate(s6.x, s6.y);
    beginShape();
    lights[5].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 7 symbol
    pushMatrix();
    translate(s7.x, s7.y);
    beginShape();
    lights[6].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 8 symbol
    pushMatrix();
    translate(s8.x, s8.y);
    beginShape();
    lights[7].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 9 symbol
    pushMatrix();
    translate(s9.x, s9.y);
    beginShape();
    lights[8].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 10 symbol
    pushMatrix();
    translate(s10.x, s10.y);
    beginShape();
    lights[9].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 11 symbol
    pushMatrix();
    translate(s11.x, s11.y);
    lights[10].display();
    beginShape();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();

    // 12 symbol
    pushMatrix();
    translate(s12.x, s12.y);
    beginShape();
    lights[11].display();
    texture(sand);
    for (int i=0; i<circleResolution; i++) {
      float angle = theta * i;
      float x = cos(angle);
      float y = sin(angle);
      vertex(x * diam, y * diam, x * diam, y * diam);
    }
    endShape(CLOSE);
    popMatrix();
  }
}