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

	// Get Symbol glow GameObject with script
	public Glow symbolGlow;
	// Selected symbol trigger
	public bool[] symbolIsSelected = new bool[12];

	// Fade in / out Opacity 
	float[] opacityValue = new float[12];
	float opacityTarget = 1f;
	float[] opacitySpeed = new float[12];

	public bool[] doorIsOpen = new bool[4];
	
	void Start () {
		// Create all GameObjects from prefabs and set attributes
		for (int i = 0; i<12; i++) {
			// Instanciate a black symbol prefab as new GameObject
			GameObject symbol = GameObject.Instantiate (symbolPrefab [i])as GameObject;
			// Instanciate a white glow symbol prefab as new GameObject
			GameObject glow = GameObject.Instantiate (symbolGlowPrefab [i])as GameObject;
			// Move the instance to his parent
			symbol.transform.parent = GameObject.FindGameObjectWithTag ("rock" + (i + 1)).transform;
			glow.transform.parent = GameObject.FindGameObjectWithTag ("rock" + (i + 1)).transform;
			// Set position from parent
			symbol.transform.localPosition = new Vector3 (0, 0, 0);
			glow.transform.localPosition = new Vector3 (0, 0, 0);
			// Set scale from parent
			symbol.transform.localScale = new Vector3 (0.06f, 0.06f, 0.06f);
			glow.transform.localScale = new Vector3 (0.06f, 0.06f, 0.06f);
			// Apply instance of prefab to a GameObject
			symbols [i] = symbol;
			symbolsGlow [i] = glow;
			// Set starting speed & value
			opacitySpeed [i] = Random.Range (0.002f, 0.01f);
			opacityValue [i] = 0f;
			// Glow effect at opacity 0
			symbolsGlow [i].GetComponent<Renderer> ().material.color = new Color (255, 255, 255, 0);
			// All symbols starts not selected
			symbolIsSelected [i] = false;
		}
		for (int i = 0; i < 4; i++) {
			doorIsOpen [i] = false;
		}
		// Set a random position to the GameObjects in the game
		shuffle();
	}
	void Update () {

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

	public void door(int index, bool door){
		// Check if door is open
		for (int i = 0; i < 4; i++) {
				doorIsOpen[i] = door;
		}

	}

	public void selectedSymbol (int index, bool selectSide){
		
		if (selectSide == false) {
			symbolIsSelected [index] = selectSide;
			return;
		}

		if (doorIsOpen [0] == true) {
			if (index < 3) {
				if (symbolIsSelected [0] == false && symbolIsSelected [1] == false && symbolIsSelected [2] == false) {
					symbolIsSelected [index] = selectSide;
				}
			}
			else if (doorIsOpen [1] == true) {
				  if (index < 6) {
					if (symbolIsSelected [3] == false && symbolIsSelected [4] == false && symbolIsSelected [5] == false) {
						symbolIsSelected [index] = selectSide;
					}
				}
				else if (doorIsOpen [2] == true) {
					 if ( index < 9){
						
						if (symbolIsSelected[6] == false && symbolIsSelected[7] == false && symbolIsSelected[8] == false){
							symbolIsSelected[index] = selectSide;
						}
					}
					else if(doorIsOpen [3] == true) {
							if (symbolIsSelected[9] == false && symbolIsSelected[10] == false && symbolIsSelected[11] == false){
								symbolIsSelected[index] = selectSide;

						}
					}
				}
			}
		}
	}
	void fadeIn(){
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
	void fadeOut(){
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
			// When ended...
			// Reset the game
			shuffle();
		}
	}
}
