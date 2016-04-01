using UnityEngine;
using System.Collections;

public class spotlight_flicker : MonoBehaviour {

	Color[] color = new Color[2];
	public Light light;
	float r, g, b;
	Color light_color;
	float speed;
	float lerpValue;


	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		for (int i = 0; i < 2; i++) {
			color [i] = new Color (0, 0, 0);
		}
		speed = 5;
	}

	// Update is called once per frame
	void Update () {
		
		lerpValue = Mathf.PingPong (Time.time * speed, 1);

		light.color = Color.Lerp(color[0], color[1], lerpValue);

		if (lerpValue <=0.1) {

			r = Random.Range (20, 50);
			g = Random.Range (140, 150);
			b = Random.Range (150, 180);

			color [1] = new Color (r, g, b, 1);

		} else if (lerpValue >= 0.9) {

			r = Random.Range (20, 50);
			g = Random.Range (140, 150);
			b = Random.Range (150, 180);

			color [0] = new Color (r, g, b, 1);
		}
	
	}
}
