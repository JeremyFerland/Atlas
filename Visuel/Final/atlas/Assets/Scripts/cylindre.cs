using UnityEngine;
using System.Collections;

public class cylindre : MonoBehaviour {

	public bool isCreating = false;
	public Vector2 targetPosition;
	public Vector2 initialPosition;
	public bool isFinish=false;
	float removeThatToScale;
	Vector3 eulerAngle = new Vector3 (-1000,0,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isCreating) {
			if(initialPosition.x != targetPosition.x){
				if(eulerAngle == new Vector3 (-1000,0,0)){
					transform.localEulerAngles = new Vector3(0,0,270);
					eulerAngle = transform.localEulerAngles;
					removeThatToScale = initialPosition.x;
				}
				float scaleX = Mathf.MoveTowards(initialPosition.x, targetPosition.x, 0.05f);
				if(scaleX-removeThatToScale == 0){
					return;
				}
				initialPosition = new Vector2(scaleX,initialPosition.y);
				transform.localScale = new Vector3(0.2f,scaleX-removeThatToScale,0.2f);
				if(Mathf.Round(initialPosition.x *1000)  == Mathf.Round(targetPosition.x*1000)){
					isFinish = true;
				}

			}else if (initialPosition.y != targetPosition.y){
				if(eulerAngle == new Vector3 (-1000,0,0)){
					transform.localEulerAngles = new Vector3(0,0,0);
					eulerAngle = transform.localEulerAngles;	
					removeThatToScale = initialPosition.y;
				}
				float scaleY = Mathf.MoveTowards(initialPosition.y, targetPosition.y, 0.05f);
				initialPosition = new Vector2(initialPosition.x,scaleY);
				if(scaleY-removeThatToScale == 0){
					return;
				}
				transform.localScale = new Vector3(0.2f,scaleY-removeThatToScale,0.2f);
				if(Mathf.Round(initialPosition.y * 1000)  == Mathf.Round(targetPosition.y * 1000)){
					isFinish = true;
				}
			}
		
		}

	}
}
