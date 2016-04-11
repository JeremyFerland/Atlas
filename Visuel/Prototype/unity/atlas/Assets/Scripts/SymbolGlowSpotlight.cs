using UnityEngine;
using System.Collections;

public class SymbolGlowSpotlight : MonoBehaviour {
	
	public GameObject spotlightPrefab;
	public GameObject[] lights = new GameObject[12];
	public Light[] bulb = new Light[12];

	public float[] intensityValue = new float[12];
	float intensitySpeed = 0.05f;
	float intensityTarget = 1;
	public float oscillationValue = 0;
	float oscillationScale = 2f;


	public bool[] selectSymbol = new bool[12];

	// Use this for initialization
	void Start () {
		// Create all GameObjects from prefabs
		for (int i = 0; i<12; i++) {
			// Create a new object from prefab
			GameObject light = GameObject.Instantiate(spotlightPrefab)as GameObject;
			// PLace in parent
			light.transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			// Set position
			light.transform.localPosition = new Vector3(0,0,60);
			// Assing prefab to a new GameObject
			lights[i] = light;
			// Get the light
			bulb[i] = FindObjectOfType<Light>();
			// Set intensity
			bulb[i].intensity = 0;
			intensityValue[i] = 0;
			// Set all bulbs at off
			selectSymbol[i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		// LFO value normalised + scale
		oscillationValue = (Mathf.Sin(Time.time*4)*2)+4+oscillationScale;

		fadein ();
	}

	public void fadein(){


		// Update all intensity
		for (int i = 0; i <12; i++) {

			// If selected
			if(selectSymbol[i] == true){
				if(intensityValue[i] < intensityTarget){
					// Add intensity
					intensityValue[i] += intensitySpeed;
				}
				// If not selected
			} else if (selectSymbol[i] == false) {
				// Less intensity
				if (intensityValue[i]> 0){
					intensityValue[i] -= intensitySpeed;
				}
			}
			// Update intensity value
			bulb[i].intensity = intensityValue[i]*oscillationValue;
		}
	}
}