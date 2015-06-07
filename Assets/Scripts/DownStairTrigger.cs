using UnityEngine;
using System.Collections;

public class DownStairTrigger : MonoBehaviour {

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
			GameController.wentDown = true;
			GameController.curentLevel++;
				}

		}

	private FloorCell currentCell;
	
	public void GetLocation (FloorCell cell) {
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}
}
