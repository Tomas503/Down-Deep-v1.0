using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public DownStairTrigger downStairTriggerPrefab;
	private DownStairTrigger downStairTriggerInstance;
	public UpStairTrigger upStairTriggerPrefab;
	private UpStairTrigger upStairTriggerInstance;
	private Vector3 aheadOfPlayer;
	private Vector3 behindPlayer;





	// Use this for initialization
	void Start () {

		GetPlayerPositions ();
		if (GameController.curentLevel == 0) {

			MakeDownStair ();
			}
		if (GameController.curentLevel > 0) {
			MakeUpStair ();
			MakeDownStair ();

			}
	
	}
	
	// Update is called once per frame
	void Update () {

		//GetPlayerPositions ();

	



	}

	void MakeDownStair(){
		downStairTriggerInstance = Instantiate(downStairTriggerPrefab)as DownStairTrigger;
		downStairTriggerInstance.transform.Translate(aheadOfPlayer);
		downStairTriggerInstance.transform.parent = gameObject.transform;
	}

	void MakeUpStair(){
		upStairTriggerInstance = Instantiate(upStairTriggerPrefab)as UpStairTrigger;
		//upStairTriggerInstance.transform.Translate(0,0,-forwardDistance);
		upStairTriggerInstance.transform.Translate(behindPlayer);
		upStairTriggerInstance.transform.parent = gameObject.transform;

	}
	void GetPlayerPositions(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		behindPlayer = (player.transform.position - player.transform.forward * 5.0f);
		aheadOfPlayer = (player.transform.position + player.transform.forward * 5.0f);
		}


}
