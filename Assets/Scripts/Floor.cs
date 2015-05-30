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

	//public int sizeX, sizeZ;

	public FloorCell cellPrefab;
	private FloorCell[,] cells;

	public IntVector2 size;

	public FloorPassage passagePrefab;
	public FloorWall wallPrefab;


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
		List<FloorCell> activeCells = new List<FloorCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			DoNextGenerationStep(activeCells);
		}
	}


	private void DoFirstGenerationStep (List<FloorCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}
	
	private void DoNextGenerationStep (List<FloorCell> activeCells) {
				int currentIndex = activeCells.Count - 1;
				FloorCell currentCell = activeCells [currentIndex];
				FloorDirection direction = FloorDirections.RandomValue;
				IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();
				if (ContainsCoordinates (coordinates)) {
						FloorCell neighbor = GetCell (coordinates);
						if (neighbor == null) {
								neighbor = CreateCell (coordinates);
								CreatePassage (currentCell, neighbor, direction);
								activeCells.Add (neighbor);
						} else {
								CreateWall (currentCell, neighbor, direction);
								activeCells.RemoveAt (currentIndex);
						}
				} else {
						CreateWall (currentCell, null, direction);
						activeCells.RemoveAt (currentIndex);
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
		FloorPassage passage = Instantiate(passagePrefab) as FloorPassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as FloorPassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}
	
	private void CreateWall (FloorCell cell, FloorCell otherCell, FloorDirection direction) {
		FloorWall wall = Instantiate(wallPrefab) as FloorWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as FloorWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

}
