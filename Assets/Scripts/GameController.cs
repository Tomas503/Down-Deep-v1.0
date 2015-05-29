using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

	public Player playerPrefab;
	public Player playerInstance;
	public Floor floorPrefab;
	private Floor floorInstance;
	private bool needNewFloor = false;

	public static bool destroyThings = false;
	public static int curentLevel;
	public static int lastLevel;
	public static Transform playerLocation;
	// Use this for initialization
	void Start () {

		BeginGame ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyThings & floorInstance) {
			DestroyFloor();
			needNewFloor = true;
			destroyThings = false;
		}
		if (needNewFloor) {
			MakeFloor();
			needNewFloor = false;
				}


		lastLevel = curentLevel;
	}

	void BeginGame(){
		curentLevel = 0;
		lastLevel = 0;
		MakeFloor ();
		MakePlayer ();
	}

	void MakeFloor(){
		floorInstance = Instantiate (floorPrefab) as Floor;
		Color col = Random.Range (0, 2) == 1 ? Color.black : Color.white;
		floorInstance.GetComponent<MeshRenderer>().material.color = col;



	}
	
	void MakePlayer(){
		playerInstance = Instantiate(playerPrefab) as Player;
		playerLocation = playerInstance.transform;
		}

	void DestroyFloor(){
		Destroy(floorInstance.gameObject);
		//if (DownStairTrigger) {
		//	Destroy(GameObject.Find  ("DnStair"));
		//	}
    }

}
