﻿using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public DownStairTrigger downStairTriggerPrefab;
	private DownStairTrigger downStairTriggerInstance;
	public UpStairTrigger upStairTriggerPrefab;
	private UpStairTrigger upStairTriggerInstance;
	private Vector3 aheadOfPlayer;
	private Vector3 behindPlayer;

	//public int sizeX, sizeZ;

	public FloorCell cellPrefab;
	private FloorCell[,] cells;

	public IntVector2 size;




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
		cells = new FloorCell[size.x, size.z];
		for (int x = 0; x < size.x; x++) {
			for (int z = 0; z < size.z; z++) {
				CreateCell(new IntVector2(x, z));
			}
		}
	}
	
	private void CreateCell (IntVector2 coordinates) {
		FloorCell newCell = Instantiate(cellPrefab) as FloorCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Floor Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
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
