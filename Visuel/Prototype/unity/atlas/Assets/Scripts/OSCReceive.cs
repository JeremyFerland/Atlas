using UnityEngine;
using System.Collections;

public class OSCReceive : MonoBehaviour {

	// OSC main script
	public OSC oscReference;


	Symbol symbols;
	public SymbolGlowSpotlight symbolGlowSpot;

	// Use this for initialization
	void Start () {
		oscReference.SetAllMessageHandler(OnReceive);
	}

	void OnReceive(OscMessage message){

		// Restart the game
		if (message.address == "/Success") {
			float data = message.GetFloat (0);
			if (data == 1){
			symbols.fadeout ();
			}
		}
		// Select a symbol
		for (int i = 0; i <12; i++) {
			if (message.address == "/Symbol"+i) {
				Debug.Log(message.address);
				float data = message.GetFloat (0);
				Debug.Log(data);
				if (data == 1) {
					// Glow selected symbol
					symbolGlowSpot.selectSymbol[i] = true;
				} else {
					// Unglow selected symbol
					symbolGlowSpot.selectSymbol [i] = false;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void send(string message){

		OscMessage tempMessage = new OscMessage ();
		tempMessage = OSC.StringToOscMessage (message);
		oscReference.Send (tempMessage);

	}
}
