using UnityEngine;
using System.Collections;

public class symbol : MonoBehaviour {

	public GameObject[] prefab;
	public GameObject[] symbols = new GameObject[12];

	float opacityValue = 0f;
	float opacityTarget = 1f;
	float opacitySpeed = 0.01f;

	// Use this for initialization
	void Start () {
	
		for (int i = 0; i<12; i++) {
			GameObject symbol = GameObject.Instantiate(prefab[i])as GameObject;
			symbol.transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			symbol.transform.localPosition = new Vector3(0,0,0);
			symbol.transform.localScale = new Vector3(0.06f,0.06f,0.06f);
			symbols[i] = symbol;
		}
		shuffle();
	}
	
	// Update is called once per frame
	void Update () {

		fadein();
	
	}
	void shuffle(){

		for (int i = 0; i<12; i++) {
			GameObject temp = symbols[i];
			int randomIndex = Random.Range(0,12);
			symbols[i] = symbols[randomIndex];
			symbols[randomIndex] = temp;
		}
		for (int i = 0; i<12; i++) {
			symbols[i].transform.parent = GameObject.FindGameObjectWithTag("rock"+(i+1)).transform;
			symbols[i].transform.localPosition = new Vector3(0,0,-1f);

			if(i <3){
				symbols[i].transform.localEulerAngles = new Vector3 (-90, 0, 0);
			} else if ( i < 6){
				symbols[i].transform.localEulerAngles = new Vector3 (0, 90, -90);
			} else if ( i < 9){
				symbols[i].transform.localEulerAngles = new Vector3 (90, 180, 0);
			} else {
				symbols[i].transform.localEulerAngles = new Vector3 (0, -90, 90);
			}
		}

	}


	void fadein(){

		if (opacityValue < opacityTarget) {
			Debug.Log(opacityValue);
			opacityValue+=opacitySpeed;
		}
		for (int i = 0; i < 12; i++) {
			symbols[i].GetComponent<Renderer> ().material.color = new Color (255, 255, 255, opacityValue);
		}
	}

}
