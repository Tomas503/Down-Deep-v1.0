using UnityEngine;
using System.Collections.Generic;

public class FloorRoom : ScriptableObject {

	public int settingsIndex;
	
	public FloorRoomSettings settings;
	
	private List<FloorCell> cells = new List<FloorCell>();
	
	public void Add (FloorCell cell) {
		cell.room = this;
		cells.Add(cell);
	}
}
