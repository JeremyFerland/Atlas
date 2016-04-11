using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {

	// Get Symbol GameObject with script
	public Symbol symbol;
	// Under spotlight prefab
	public GameObject spotlightPrefab;
	// Spotlight GameObject
	GameObject[] spotlight = new GameObject[12];
	// Light attribute of GameObject
	Light[] bulb = new Light[12];
	// Intensity value
	float[] intensityValue = new float[12];
	float intensitySpeed = 0.05f;
	float intensityTarget = 1;
	// Oscillation value
	float oscillationValue = 0;
	float oscillationScale = 2f;
	
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
			spotlight[i] = light;
			// Get the light
			bulb[i] = FindObjectOfType<Light>();
			// Set intensity
			bulb[i].intensity = 0;
			intensityValue[i] = 0;
		}
	}

	void Update () {
		// LFO value normalised + scale
		oscillationValue = (Mathf.Sin(Time.time*4)*2)+4+oscillationScale;
		for (int i = 0; i < 12; i++) {
			// If a symbol is selected, il will also activate the glow plane with the same oscillation pattern
			symbol.symbolsGlow [i].GetComponent<Renderer> ().material.color = new Color (255, 255, 255, (symbol.symbolGlow.intensityValue [i] * symbol.symbolGlow.oscillationValue) / 12);
		}
		// Glow if selected
		glow();
	}

	void glow(){
		// Update all intensity
		for (int i = 0; i <12; i++) {
			// If selected
			if(symbol.symbolIsSelected[i] == true){
				if(intensityValue[i] < intensityTarget){
					// Add intensity
					intensityValue[i] += intensitySpeed;
				}
				// If not selected
			} else if (symbol.symbolIsSelected[i] == false) {
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