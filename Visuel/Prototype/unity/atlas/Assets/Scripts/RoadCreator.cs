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
	public bool isGood = false;


	public void StartRoad (){
		isRoadStart = true;
		roads = new cylindre[roadPts.Length];
		nextRoad ();
	}

	public void DestroyRoad(){
		Renderer[] cylindres = GetComponentsInChildren<Renderer> ();
		foreach (Renderer temp in cylindres) {
			GameObject.Destroy(temp.gameObject);
		}
	}

	// Use this for initialization
	void Start () {

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
			if(!isGood){
				DestroyRoad();
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
	}
}
