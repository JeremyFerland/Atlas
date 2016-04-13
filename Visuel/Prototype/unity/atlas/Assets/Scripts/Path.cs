using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	public Symbol symbol;

	public GameObject cylinderPrefab = new GameObject();
	List<GameObject> path;
	int objectCount = 0;

	
	void Start () {
		path = new List<GameObject>();
	}
	

	void Update () {

		if (Input.GetKeyDown (KeyCode.A)) {
			createPath();
		}

		else if (Input.GetKeyDown (KeyCode.B)) {
			deletePath();
		}

	}

	void createPath(){
	
		path.Add(GameObject.Instantiate (cylinderPrefab) as GameObject);
		path[objectCount].transform.parent = GameObject.FindGameObjectWithTag ("path").transform;
		objectCount += 1;

	}

	void deletePath(){

		if (objectCount >= 1) {
			GameObject.Destroy (path [objectCount - 1]);
			objectCount -= 1;
		} else {
			Debug.Log("No objects to destroy");
		}
	}
}
