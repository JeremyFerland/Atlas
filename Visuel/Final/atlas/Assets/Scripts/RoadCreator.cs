using UnityEngine;
using System.Collections;

public class RoadCreator : MonoBehaviour {

	public OSCReceive osc;
	public Vector2[] roadPts;
	public GameObject roadPrefab;
	public GameObject corner;
	cylindre[] roads;
	public bool isRoadStart = false;
	public bool isRoadFinish = false;
	int index = -1;
	public bool isGood = false;
	int destroy = 0;
	bool isFadeOut = false;
	public Particles particles;
	public bool isLocked = false;

	public int sideNumber;

	float opacityValue = 1;


	public void StartRoad (){
		isRoadStart = true;
		roads = new cylindre[roadPts.Length];
		index = -1;
		destroy = 1;
		isFadeOut = false;
		opacityValue = 1;
		nextRoad ();
	}

	public void destroyRoadWithDoor(){
		Renderer[] cylindres = GetComponentsInChildren<Renderer> ();

		if (opacityValue > 0) {
			opacityValue -= 0.01f;
			foreach (Renderer temp in cylindres) {
				temp.material.color = new Color (temp.material.color.r, temp.material.color.g, temp.material.color.b, opacityValue);
			}
			return;
		} else if (opacityValue == 0) {
			osc.send ("/RoadHit" + " " + 1); // 1 = destroy
		}
			foreach (Renderer temp in cylindres) {
				GameObject.Destroy (temp.gameObject);
				particles.pitchParticles (sideNumber);
			}
			isRoadStart = false;
			isRoadFinish = false;
			index = -1;
			isFadeOut = false;
	}

	void destroyRoadLocal(){
		if (isLocked) {
			return;
		}
		Renderer[] cylindres = GetComponentsInChildren<Renderer> ();
		if (destroy == 1) {
			particles.pitchParticles(sideNumber);
			destroy = 0;
		}
		foreach (Renderer temp in cylindres) {
			
			GameObject.Destroy(temp.gameObject);
		}
		osc.send ("/RoadHit" + sideNumber + " " + 1); // 1 = destroy
		isRoadStart = false;
		isRoadFinish = false;
		index = -1;
		isFadeOut = false;
	}

	public void DestroyRoad(){
		if (!isLocked) {
			isFadeOut = true;
		}
	}

	void fadeout(){
		Renderer[] cylindres = GetComponentsInChildren<Renderer> ();
		if (opacityValue > 0) {
			opacityValue -= 0.01f;
			foreach (Renderer temp in cylindres) {
				temp.material.color = new Color (temp.material.color.r, temp.material.color.g, temp.material.color.b, opacityValue);
			}
		} else{
			destroyRoadLocal();
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (isFadeOut) {
			fadeout ();
		}

		if (isRoadStart && !isRoadFinish) {
			if(roads[index].isFinish){
				nextRoad();
			}
		}
	
	}
	
	void nextRoad(){
		index++;
		if (index >= roadPts.Length -1) {
			isRoadFinish = true;
			if(!isGood){
				DestroyRoad();
				osc.send ("/RoadHit" + sideNumber + " " + 0); // 0 = Wrong connected one

			}
			return;
		}
		GameObject tempCorner = GameObject.Instantiate (corner) as GameObject;
		tempCorner.transform.parent = this.transform;
		tempCorner.transform.position = new Vector3(roadPts [index].x,roadPts[index].y, 1);
		GameObject tempNextRoad = GameObject.Instantiate(roadPrefab)as GameObject;
		tempNextRoad.transform.parent = this.transform;
		cylindre cylindre = tempNextRoad.GetComponent<cylindre> ();
		tempNextRoad.transform.position = new Vector3(roadPts [index].x,roadPts[index].y, 1);
		cylindre.initialPosition = roadPts[index];
		cylindre.targetPosition = roadPts[index+1];
		cylindre.isCreating = true;
		roads [index] = cylindre;
		tempCorner.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);

		osc.send ("/Road" + sideNumber + " " + cylindre.initialPosition.x+ " " + cylindre.initialPosition.y);
	}

	public void endGame(){

		Renderer[] cylindres = GetComponentsInChildren<Renderer> ();

		foreach (Renderer temp in cylindres) {
			GameObject.Destroy(temp.gameObject);
		}
		isGood = false;
		isRoadStart = false;
		isRoadFinish = false;
		index = -1;
		isFadeOut = false;
		isLocked = false;

	}
}
