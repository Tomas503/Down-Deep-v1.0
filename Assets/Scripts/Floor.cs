using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public DownStairTrigger downStairTriggerPrefab;
	private DownStairTrigger downStairTriggerInstance;
	public UpStairTrigger upStairTriggerPrefab;
	private UpStairTrigger upStairTriggerInstance;
	private Vector3 aheadOfPlayer;
	private Vector3 behindPlayer;

	public int sizeX, sizeZ;

	public FloorCell cellPrefab;
	private FloorCell[,] cells;




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


	public void Generate () {
		cells = new FloorCell[sizeX, sizeZ];
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				CreateCell(x, z);
			}
		}
	}
	
	private void CreateCell (int x, int z) {
		FloorCell newCell = Instantiate(cellPrefab) as FloorCell;
		cells[x, z] = newCell;
		newCell.name = "Floor Cell " + x + ", " + z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(x - sizeX * 0.5f + 0.5f, 0f, z - sizeZ * 0.5f + 0.5f);
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
