using UnityEngine;
using System.Collections;

public class ambientLight_flicker : MonoBehaviour {
	
	Color[] color = new Color[2];
	public Light light;
	float intensityValue = 0;
	public float intensityTarget = 0.03f;
	public float intensitySpeed = 0.001f;
	float r, g, b;
	Color light_color;
	public float flickerSpeed = 1;
	float lerpValue;
	
	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		for (int i = 0; i < 2; i++) {
			color [i] = new Color (0, 0, 0);
		}
	}

	// Update is called once per frame
	void Update () {

		fadein();

		lerpValue = Mathf.PingPong (Time.time * flickerSpeed, 1);

		light.color = Color.Lerp(color[0], color[1], lerpValue);

		if (lerpValue <=0.1) {

			r = Random.Range (6, 12);
			g = Random.Range (3, 9);
			b = Random.Range (36, 46);;

			color [1] = new Color (r, g, b, 1);

		} else if (lerpValue >= 0.9) {

			r = Random.Range (6, 12);
			g = Random.Range (3, 9);
			b = Random.Range (36, 46);

			color [0] = new Color (r, g, b, 1);
		}
	}
	void fadein(){

		if (light.intensity < intensityTarget) {
		
			intensityValue += intensitySpeed;
		}

		light.intensity = intensityValue;
	}

}