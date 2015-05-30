using UnityEngine;


public enum FloorDirection {
	North,
	East,
	South,
	West


}

public static class FloorDirections {
	
	public const int Count = 4;
	
	public static FloorDirection RandomValue {
		get {
			return (FloorDirection)Random.Range(0, Count);
		}
	}


	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};
	
	public static IntVector2 ToIntVector2 (this FloorDirection direction) {
		return vectors[(int)direction];
	}
}
