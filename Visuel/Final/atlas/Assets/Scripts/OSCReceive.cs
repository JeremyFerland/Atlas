using UnityEngine;
using System.Collections;

public class OSCReceive : MonoBehaviour {

	// Get OSC GameObject with script
	public OSC oscReference;
	// Get Symbol GameObject with script
	public Symbol symbol;

	void Start () {
		oscReference.SetAllMessageHandler(OnReceive);
	}

	void OnReceive(OscMessage message){


		for (int i = 0; i < 4; i++) {
			if(message.address == "/Door"+i){
				float data = message.GetFloat (0);
				if(data == 1) {
					// Set door as open
					symbol.doorIsOpen[i] = true;
				} else if (data == 0){
					// Set door as closed
					symbol.doorIsOpen[i] = false;
				}
			}

		}

		// Select a symbol
		for (int i = 0; i <12; i++) {
			if (message.address == "/Symbol"+i) {
				// Get 1st value
				float data = message.GetFloat (0);
				if (data == 1) {
					// Set as selected symbol
					symbol.selectedSymbol(i,true);
				} else if (data == 0) {
					// Set as non-selected symbol
					symbol.selectedSymbol(i,false);
				}
			}
		}
	}

	void Update () {


	}

	public void send(string message){
		OscMessage tempMessage = new OscMessage ();
		tempMessage = OSC.StringToOscMessage (message);
		oscReference.Send (tempMessage);
	}
}
