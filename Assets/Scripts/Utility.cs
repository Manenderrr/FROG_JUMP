using UnityEngine;

public static class VectorExtensions {
	public static Vector2 To2D(this Vector3 obj) => new(obj.x, obj.y);
}