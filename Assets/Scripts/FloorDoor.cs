﻿using UnityEngine;


public class FloorDoor : FloorPassage {

	public Transform hinge;


	private FloorDoor OtherSideOfDoor {
		get {
			return otherCell.GetEdge(direction.GetOpposite()) as FloorDoor;
		}
	}
	
	public override void Initialize (FloorCell primary, FloorCell other, FloorDirection direction) {
		base.Initialize(primary, other, direction);
		if (OtherSideOfDoor != null) {
			hinge.localScale = new Vector3(-1f, 1f, 1f);
			Vector3 p = hinge.localPosition;
			p.x = -p.x;
			hinge.localPosition = p;
		}
	}
}
