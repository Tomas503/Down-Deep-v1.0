using UnityEngine;
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

	public void OnPlayerEntered () {
		for (int i = 0; i < edges.Length; i++) {
			edges[i].OnPlayerEntered();
		}
	}
	
	public void OnPlayerExited () {
		for (int i = 0; i < edges.Length; i++) {
			edges[i].OnPlayerExited();
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

	public FloorRoom room;
	
	public void Initialize (FloorRoom room) {
		room.Add(this);
		transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
	}


}
