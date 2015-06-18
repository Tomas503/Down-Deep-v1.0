using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {

	private FloorCell currentCell;
	
	public void SetLocation (FloorCell cell) {
		if (currentCell != null) {
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
		currentCell.OnPlayerEntered();
	}

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
