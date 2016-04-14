using UnityEngine;
using System.Collections;

public class RoadCreator : MonoBehaviour {

	public Vector2[] roadPts;
	public GameObject roadPrefab;
	public GameObject corner;
	cylindre[] roads;
	public bool isRoadStart = false;
	public bool isRoadFinish = false;
	int index = -1;


	public void StartRoad (){
		isRoadStart = true;
		nextRoad ();
	}

	public void DestroyRoad(){
	
	}

	// Use this for initialization
	void Start () {
		roads = new cylindre[roadPts.Length];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			StartRoad();
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
			return;
		}
		GameObject tempCorner = GameObject.Instantiate (corner) as GameObject;
		tempCorner.transform.parent = this.transform;
		tempCorner.transform.position = roadPts [index];
		GameObject tempNextRoad = GameObject.Instantiate(roadPrefab)as GameObject;
		tempNextRoad.transform.parent = this.transform;
		cylindre cylindre = tempNextRoad.GetComponent<cylindre> ();
		tempNextRoad.transform.position = roadPts [index];
		cylindre.initialPosition = roadPts[index];
		cylindre.targetPosition = roadPts[index+1];
		cylindre.isCreating = true;
		roads [index] = cylindre;
		tempCorner.transform.localScale = new Vector3 (1, 1, 1);
	}
}
