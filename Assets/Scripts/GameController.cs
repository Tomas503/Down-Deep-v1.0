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
	//[System.NonSerialized}
	 public static bool wentDown;
	 
	 //[System.NonSerialized}
	 public static bool wentUp;
	// Use this for initialization
	void Start () {

		BeginGame ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			RestartGame();
				}
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
		Camera.main.rect = new Rect(0f, 0f, 0.16f, 0.3f);
		MakeFloor ();
		MakePlayer ();
	}
	void RestartGame(){
		DestroyFloor ();
		Destroy (playerInstance.gameObject);

		BeginGame ();
		}

	void MakeFloor(){
		floorInstance = Instantiate (floorPrefab) as Floor;
		floorInstance.Generate ();
		//Color col = Random.Range (0, 2) == 1 ? Color.black : Color.white;
		//floorInstance.GetComponent<MeshRenderer>().material.color = col;
		}


	private Vector3 tempPlayerShift;

	void MakePlayer(){
		playerInstance = Instantiate(playerPrefab) as Player;
		//playerLocation = playerInstance.transform;
		playerInstance.SetLocation(floorInstance.GetCell(floorInstance.RandomCoordinates));
		tempPlayerShift = playerInstance.transform.position;
		tempPlayerShift.y = 1.5f;
		playerInstance.transform.position = tempPlayerShift;
		}

	void DestroyFloor(){
		Destroy(floorInstance.gameObject);
		//if (DownStairTrigger) {
		//	Destroy(GameObject.Find  ("DnStair"));
		//	}
    }

}
