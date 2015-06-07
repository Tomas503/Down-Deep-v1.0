using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

	public DownStairTrigger downStairTriggerPrefab;
	private DownStairTrigger downStairTriggerInstance;
	public UpStairTrigger upStairTriggerPrefab;
	private UpStairTrigger upStairTriggerInstance;
	private Vector3 aheadOfPlayer;
	private Vector3 behindPlayer;

	public FloorRoomSettings[] roomSettings;

	public FloorCell cellPrefab;
	private FloorCell[,] cells;

	public IntVector2 size;

	public FloorPassage passagePrefab;
	public FloorWall[] wallPrefabs;

	public FloorDoor doorPrefab;
	
	[Range(0f, 1f)]
	public float doorProbability;



	// Use this for initialization
	void Start () {

		GetPlayerPositions ();
		if (GameController.curentLevel == 0) {

			MakeDownStair (RandomCoordinates);
			}
		if (GameController.curentLevel > 0) {
			MakeUpStair (RandomCoordinates);
			MakeDownStair (RandomCoordinates);

			}
	
	}
	
	// Update is called once per frame
	void Update () {

		//GetPlayerPositions ();

	}


	public void Generate () {
		cells = new FloorCell[size.x, size.z];
		List<FloorCell> activeCells = new List<FloorCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			DoNextGenerationStep(activeCells);
		}
	}


	private void DoFirstGenerationStep (List<FloorCell> activeCells) {
		FloorCell newCell = CreateCell(RandomCoordinates);
		newCell.Initialize(CreateRoom(-1));
		activeCells.Add(newCell);
	}
	
	private void DoNextGenerationStep (List<FloorCell> activeCells) {
				int currentIndex = activeCells.Count - 1;
				FloorCell currentCell = activeCells [currentIndex];
				if (currentCell.IsFullyInitialized) {
						activeCells.RemoveAt (currentIndex);
						return;
				}
				FloorDirection direction = currentCell.RandomUninitializedDirection;
				IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();
				if (ContainsCoordinates (coordinates)) {
						FloorCell neighbor = GetCell (coordinates);
						if (neighbor == null) {
								neighbor = CreateCell (coordinates);
								CreatePassage (currentCell, neighbor, direction);
								activeCells.Add (neighbor);
						}
						else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex) {
								CreatePassageInSameRoom(currentCell, neighbor, direction);
						} 
						else {
								CreateWall (currentCell, neighbor, direction);
						}
				} 
				else {
						CreateWall (currentCell, null, direction);
						
				}
		}

	private void CreatePassageInSameRoom (FloorCell cell, FloorCell otherCell, FloorDirection direction) {
		FloorPassage passage = Instantiate(passagePrefab) as FloorPassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as FloorPassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
		if (cell.room != otherCell.room) {
			FloorRoom roomToAssimilate = otherCell.room;
			cell.room.Assimilate(roomToAssimilate);
			rooms.Remove(roomToAssimilate);
			Destroy(roomToAssimilate);
		}
	}
	

	private FloorCell CreateCell (IntVector2 coordinates) {
		FloorCell newCell = Instantiate(cellPrefab) as FloorCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Floor Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}




	void MakeDownStair(IntVector2 stairCoordinates){
		downStairTriggerInstance = Instantiate(downStairTriggerPrefab)as DownStairTrigger;
		if (GameController.wentUp == true) {
			downStairTriggerInstance.transform.Translate (behindPlayer);
			GameController.wentUp = false;
		}
		else {
			downStairTriggerInstance.transform.localPosition = new Vector3((stairCoordinates.x - size.x * 0.5f + 0.5f)*4f, 0f, (stairCoordinates.z - size.z * 0.5f + 0.5f)*4f);
				}
		downStairTriggerInstance.transform.parent = gameObject.transform;
	}
	

	void MakeUpStair(IntVector2 stairCoordinates){
		upStairTriggerInstance = Instantiate(upStairTriggerPrefab)as UpStairTrigger;
		if (GameController.wentDown == true) {
			upStairTriggerInstance.transform.Translate (behindPlayer);
			GameController.wentDown = false;
		}
		else {
			upStairTriggerInstance.transform.localPosition = new Vector3((stairCoordinates.x - size.x * 0.5f + 0.5f)*4f, 0f, (stairCoordinates.z - size.z * 0.5f + 0.5f)*4f);
			
				}
		upStairTriggerInstance.transform.parent = gameObject.transform;

	}
	void GetPlayerPositions(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		behindPlayer = (player.transform.position - player.transform.forward * 3.0f);
		aheadOfPlayer = (player.transform.position + player.transform.forward * 3.0f);
		}

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}
	
	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public FloorCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	private void CreatePassage (FloorCell cell, FloorCell otherCell, FloorDirection direction) {
		FloorPassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
		FloorPassage passage = Instantiate(prefab) as FloorPassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(prefab) as FloorPassage;
		if (passage is FloorDoor) {
			otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
		}
		else {
			otherCell.Initialize(cell.room);
		}
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}
	
	private void CreateWall (FloorCell cell, FloorCell otherCell, FloorDirection direction) {
		FloorWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as FloorWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as FloorWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	private List<FloorRoom> rooms = new List<FloorRoom>();
	
	private FloorRoom CreateRoom (int indexToExclude) {
		FloorRoom newRoom = ScriptableObject.CreateInstance<FloorRoom>();
		newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
		if (newRoom.settingsIndex == indexToExclude) {
			newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
		}
		newRoom.settings = roomSettings[newRoom.settingsIndex];
		rooms.Add(newRoom);
		return newRoom;
	}

}
