using UnityEngine;
using System.Collections;

public class Symbol : MonoBehaviour {

	public GameObject[] prefab;
	public GameObject[] symbols = new GameObject[12];

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
			GameObject symbol = GameObject.Instantiate(prefab[i])as GameObject;
			symbol.transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			// Set position in parent
			symbol.transform.localPosition = new Vector3(0,0,0);
			// Set scale in parent
			symbol.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
			symbols[i] = symbol;
			// Set starting speed & value
			opacitySpeed[i] = Random.Range(0.002f, 0.01f);
			opacityValue[i] = 0f;
		}
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
		if (Input.GetKey (KeyCode.A)) {
			fadeoutGo = true;

		} else {
			fadeinGo = true;
		}
	}
	public void shuffle(){
		// Shuffle the symbols
		for (int i = 0; i<12; i++) {
			GameObject temp = symbols[i];
			int randomIndex = Random.Range(0,12);
			symbols[i] = symbols[randomIndex];
			symbols[randomIndex] = temp;
		}
		for (int i = 0; i<12; i++) {
			// Place the new GameObject in his parent
			symbols[i].transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			// Layer placement
			symbols[i].transform.localPosition = new Vector3(0,0,-1f);

			// Rotate symbols according of his side
			if(i <3){
				// Up symbols
				symbols[i].transform.localEulerAngles = new Vector3 (-90, 0, 0);
			} else if ( i < 6){
				// Rigth symbols
				symbols[i].transform.localEulerAngles = new Vector3 (0, 90, -90);
			} else if ( i < 9){
				// Down symbols
				symbols[i].transform.localEulerAngles = new Vector3 (90, 180, 0);
			} else {
				// Left symbols
				symbols[i].transform.localEulerAngles = new Vector3 (0, -90, 90);
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
