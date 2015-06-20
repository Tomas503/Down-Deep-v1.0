using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//RaycastHit rayCastHit;

			Debug.Log("Mouse Click");
			
			//if(Physics.Raycast(ray.origin, ray.direction, out rayCastHit, Mathf.Infinity)){
				

			//	Debug.Log("Mouse Click on door");
			//}
			
		} 
			
	}
}
