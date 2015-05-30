using UnityEngine;
using System.Collections;

public class FloorCell : MonoBehaviour {

	public IntVector2 coordinates;

	private FloorCellEdge[] edges = new FloorCellEdge[FloorDirections.Count];
	
	public FloorCellEdge GetEdge (FloorDirection direction) {
		return edges[(int)direction];
	}
	
	public void SetEdge (FloorDirection direction, FloorCellEdge edge) {
		edges[(int)direction] = edge;
	}
}
