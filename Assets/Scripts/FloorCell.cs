﻿using UnityEngine;
using System.Collections;

public class FloorCell : MonoBehaviour {

	public IntVector2 coordinates;

	private FloorCellEdge[] edges = new FloorCellEdge[FloorDirections.Count];
	
	public FloorCellEdge GetEdge (FloorDirection direction) {
		return edges[(int)direction];
	}

	private int initializedEdgeCount;
	
	public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == FloorDirections.Count;
		}
	}
	
	public void SetEdge (FloorDirection direction, FloorCellEdge edge) {
		edges[(int)direction] = edge;
		initializedEdgeCount += 1;
	}

	public FloorDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range(0, FloorDirections.Count - initializedEdgeCount);
			for (int i = 0; i < FloorDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (FloorDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("FloorCell has no uninitialized directions left.");
		}
	}
}
