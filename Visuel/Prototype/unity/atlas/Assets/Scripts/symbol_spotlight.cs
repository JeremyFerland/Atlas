using UnityEngine;
using System.Collections;

public class Symbol_spotlight : MonoBehaviour {

	// Get light component
	public Light light;
	// Create to color to lerp in
	Color[] color = new Color[2];
	// Set intensity of the light
	float intensityValue = 0;
	float intensityTarget = 3.5f;
	float intensitySpeed = 0.02f;
	// Color 
	float r, g, b;
	Color light_color;
	// Lerp flickering speed
	float flickerSpeed = 1.5f;
	float lerpValue;
	// Fade in/out control
	bool onOff = false;

	void Start () {
		// Get light component
		light = GetComponent<Light>();
		for (int i = 0; i < 2; i++) {
			// Set color to black
			color [i] = new Color (0, 0, 0);
		}
		// Start fading-in
		onOff = true;
	}
	
	void Update () {
		if (onOff == true) {
			fadeIn();
		} else {
//			fadeout();
		}
		// Flicker over time
		lerpValue = Mathf.PingPong (Time.time * flickerSpeed, 1);
		// Lerp color variation over lerp time
		light.color = Color.Lerp(color[0], color[1], lerpValue);

		if (lerpValue <=0.1) {	// If lerp starts
			// Create a new color
			r = Random.Range (6, 12);
			g = Random.Range (3, 9);
			b = Random.Range (36, 46);
			// Apply to next color
			color [1] = new Color (r, g, b, 1);
		} else if (lerpValue >= 0.9) {	// If lerp ends
			// Create a new color
			r = Random.Range (6, 12);
			g = Random.Range (3, 9);
			b = Random.Range (36, 46);
			// Apply to the next color
			color [0] = new Color (r, g, b, 1);
		}
	}

	void fadeIn(){
		// Fade in once
		if (light.intensity < intensityTarget) {
			// Add intensity
			intensityValue += intensitySpeed;
		}
		// Apply intensity
		light.intensity = intensityValue;
	}

	void fadeOut(){
		// Fade in once
		if (light.intensity > 0) {
			// Add intensity
			intensityValue -= intensitySpeed;
		}
		// Apply intensity
		light.intensity = intensityValue;
	}
}