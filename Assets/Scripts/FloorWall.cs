using UnityEngine;
using System.Collections;

public class FloorWall : FloorCellEdge {

	public Transform wall;
	
	public override void Initialize (FloorCell cell, FloorCell otherCell, FloorDirection direction) {
		base.Initialize(cell, otherCell, direction);
		wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
	}
}
