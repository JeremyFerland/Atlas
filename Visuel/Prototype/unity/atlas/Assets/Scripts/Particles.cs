using UnityEngine;
using System.Collections;

public class Particles : MonoBehaviour {

	public ParticleSystem[] particle;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pitchParticles(int side){
		StartCoroutine (pitchOff (side));
	

	}


	void pitchOff(){
		particle[0].enableEmission = true;
	}

	IEnumerator pitchOff (int side){
		particle[side].enableEmission = true;
		yield return new WaitForSeconds (1.0f);
		particle[side].enableEmission = false;
	}
}
