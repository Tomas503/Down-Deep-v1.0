﻿using UnityEngine;
using System.Collections;


public class FloorDoor : FloorPassage {

	public Transform hinge;



	private FloorDoor OtherSideOfDoor {
		get {
			return otherCell.GetEdge(direction.GetOpposite()) as FloorDoor;
		}
	}
	
	public override void Initialize (FloorCell primary, FloorCell other, FloorDirection direction) {
				base.Initialize (primary, other, direction);
				if (OtherSideOfDoor != null) {
						hinge.localScale = new Vector3 (-1f, 1f, 1f);
						Vector3 p = hinge.localPosition;
						p.x = -p.x;
						hinge.localPosition = p;
				}
				for (int i = 0; i < transform.childCount; i++) {
						Transform child = transform.GetChild (i);
						if (child != hinge) {
								child.GetComponent<Renderer> ().material = cell.room.settings.wallMaterial;
						}
				}
		}

	public override void OnPlayerEntered () {
		OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.Euler(0f, -90f, 0f);
	}
	
	public override void OnPlayerExited () {
		OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.identity;
	}
}
