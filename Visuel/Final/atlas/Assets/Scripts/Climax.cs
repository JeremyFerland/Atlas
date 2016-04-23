using UnityEngine;
using System.Collections;

public class Climax : MonoBehaviour {
	public OSCReceive osc;
	public GameObject WhitePlane;
	public bool success;
	public bool climax;
	public float opacityValue;
	public Symbol symbol;

	public PyramidSideSpotlight[] pyramidSideSpotlight;
	public SmallPyramidSpotlight[] smallPyramidSpotlight;
	public Symbol_spotlight[] symbolSpotlight;
	public RoadCreator[] roadCreator;
	public bool climaxGo = false;
	public bool successGo = false;

	// Use this for initialization
	void Start () {
		osc.send ("/Success 0");
		opacityValue = 1;
		climax = false;
		success = false;
		WhitePlane.GetComponent<Renderer> ().material.color = new Color (255, 255, 255, 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (symbol.success == true) {
			success = true;
			symbol.success = true;;
		}

		if (success == true) {
			for (int i = 0; i <4; i++) {
				pyramidSideSpotlight [i].success = true;
				smallPyramidSpotlight [i].success = true;
			}
			if(!successGo){
				successGo = true;
				Invoke ("goClimax", 5);
			}
			osc.send ("/Success 1");
		}

		if (climax == true) {
			if (opacityValue <= 1) {
				opacityValue += 0.1f;
				if(!climaxGo){
					climaxGo = true;
					Invoke ("resetGame", 0.5f);
				}
			}
			WhitePlane.GetComponent<Renderer> ().material.color = new Color (255, 255, 255, opacityValue);
		}else if (climax == false){
			if (opacityValue > 0) {
				opacityValue -= 0.1f;
			}
			WhitePlane.GetComponent<Renderer> ().material.color = new Color (255, 255, 255, opacityValue);
		}
	}

	void goClimax(){
		climax = true;
		osc.send ("/Success 0");
	}

	void resetGame(){
		for (int i = 0; i <4; i++) {
			pyramidSideSpotlight[i].success = false;
			smallPyramidSpotlight[i].success = false;
			symbol.roadCreator[i].endGame();
			symbol.success = true;
		}
		for(int j = 0; j <12; j++){
			symbolSpotlight[j].GetComponent<Light>().intensity = 0;
			symbol.symbolGlow.isLocked[j] = false;
		}
		success = false;
		climax = false;

		symbol.shuffle ();
		for (int i = 0; i < 4; i++) {
			symbol.doorIsOpen [i] = false;
		}

		symbol.success = false;
		successGo = false;
		climaxGo = false;
		osc.send ("/Success 0");

	}
}
