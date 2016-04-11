using UnityEngine;
using System.Collections;

public class Symbol : MonoBehaviour {

	// Black symbol prefab objects
	public GameObject[] symbolPrefab;
	// White glow symbol prefab objects
	public GameObject[] symbolGlowPrefab;
	// Black symbol GameObject
	public GameObject[] symbols = new GameObject[12];
	// White glow symbol GameObject
	public GameObject[] symbolsGlow = new GameObject[12];
	// Symbol glow reference
	public SymbolGlowSpotlight symbolGlowSpotlight;

	// Opacity 
	float[] opacityValue = new float[12];
	float opacityTarget = 1f;
	float[] opacitySpeed = new float[12];
	// Fade triggers
	bool fadeoutGo = false;
	bool fadeinGo = true;

	// Use this for initialization
	void Start () {
		// Create all GameObjects from prefabs
		for (int i = 0; i<12; i++) {
			// Instanciate a black symbol prefab as new GameObject
			GameObject symbol = GameObject.Instantiate(symbolPrefab[i])as GameObject;
			// Instanciate a white glow symbol prefab as new GameObject
			GameObject glow = GameObject.Instantiate(symbolGlowPrefab[i])as GameObject;
			// Move the instance to his parent
			symbol.transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			glow.transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			// Set position from parent
			symbol.transform.localPosition = new Vector3(0,0,0);
			glow.transform.localPosition = new Vector3(0,0,0);
			// Set scale from parent
			symbol.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
			glow.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
			// Apply to the GameObject
			symbols[i] = symbol;
			symbolsGlow[i] = glow;
			// Set starting speed & value
			opacitySpeed[i] = Random.Range(0.002f, 0.01f);
			opacityValue[i] = 0f;
			// Glow effect at opacity 0
			symbolsGlow[i].GetComponent<Renderer>().material.color = new Color (255, 255, 255, 0);
		}
		// Create a random position
		shuffle();
	}
	// Update is called once per frame
	void Update () {
		if (fadeoutGo == true) {
			fadeinGo = false;
			fadeout ();
		} else if (fadeinGo == true) {
			fadeoutGo = false;
			fadein ();
		}
		// Symbol glow opacity
		for (int i = 0; i <12; i++){
			// If a symbol is selected, il will also activate the glow plane with the same oscillation pattern
			symbolsGlow[i].GetComponent<Renderer>().material.color = new Color (255, 255, 255, (symbolGlowSpotlight.intensityValue[i]*symbolGlowSpotlight.oscillationValue)/12);
		}
	}
	public void shuffle(){
		// Shuffle the symbols
		for (int i = 0; i<12; i++) {
			GameObject symbolTemp = symbols[i];
			GameObject glowTemp = symbolsGlow[i];
			int randomIndex = Random.Range(0,12);
			symbols[i] = symbols[randomIndex];
			symbolsGlow[i] = symbolsGlow[randomIndex];
			symbols[randomIndex] = symbolTemp;
			symbolsGlow[randomIndex] = glowTemp;
		}
		for (int i = 0; i<12; i++) {
			// Place the new GameObject in his parent
			symbols[i].transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			symbolsGlow[i].transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			// Layer placement
			symbols[i].transform.localPosition = new Vector3(0,0,-1.5f);
			symbolsGlow[i].transform.localPosition = new Vector3(0,0,-1f);

			// Rotate symbols according of his side
			if(i <3){
				// Up symbols
				symbols[i].transform.localEulerAngles = new Vector3 (-90, 0, 0);
				symbolsGlow[i].transform.localEulerAngles = new Vector3 (-90, 0, 0);
			} else if ( i < 6){
				// Rigth symbols
				symbols[i].transform.localEulerAngles = new Vector3 (0, 90, -90);
				symbolsGlow[i].transform.localEulerAngles = new Vector3 (0, 90, -90);
			} else if ( i < 9){
				// Down symbols
				symbols[i].transform.localEulerAngles = new Vector3 (90, 180, 0);
				symbolsGlow[i].transform.localEulerAngles = new Vector3 (90, 180, 0);
			} else {
				// Left symbols
				symbols[i].transform.localEulerAngles = new Vector3 (0, -90, 90);
				symbolsGlow[i].transform.localEulerAngles = new Vector3 (0, -90, 90);
			}
		}
	}
	public void fadein(){
		fadeoutGo = false;
		// Ramp up to targetted opacity
		for(int i = 0; i < 12; i++){
			if (opacityValue[i] < opacityTarget) {
				opacityValue[i] += opacitySpeed[i];
			}
		}
		// Apply opacity
		for (int i = 0; i < 12; i++) {
			symbols[i].GetComponent<Renderer>().material.color = new Color (255, 255, 255, opacityValue[i]);
		}
	}
	public void fadeout(){
		fadeinGo = false;
		// Ramp down to 0 if greather
		for(int i = 0; i < 12; i++){
			if (opacityValue[i] > 0) {
				opacityValue[i] -= opacitySpeed[i];
			}
		}
		// Apply opacity
		for (int i = 0; i < 12; i++) {
			symbols[i].GetComponent<Renderer>().material.color = new Color (255, 255, 255, opacityValue[i]);
		}
		// Check for opacity of every symbols
		for (int i = 0; i < 12; i++){
			if(opacityValue[i] > 0){
				return;
			}
		}
		// Do it if every symbols are below opaciy "0"
		shuffle();
		fadein();
	}
}
