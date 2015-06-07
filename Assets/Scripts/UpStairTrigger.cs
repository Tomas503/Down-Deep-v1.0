using UnityEngine;
using System.Collections;

public class UpStairTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider target){
		if (target.tag == "Player") {
			Destroy(gameObject);
			GameController.destroyThings = true;
			GameController.wentUp = true;
			GameController.curentLevel--;
		}
		
	}
}
