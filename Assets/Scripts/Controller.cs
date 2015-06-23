using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Debug.Log ("Mouse Clicked");

			
			if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)){
				
				InteractiveObject obj = hit.collider.GetComponent<InteractiveObject>();
				if(obj){
					obj.TriggerInteraction();
					Debug.Log ("Mouse on Door knob");
				}

			}
			
		} 
			
	}
}
