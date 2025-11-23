using UnityEngine;

public class SmoothFollowingCamera : MonoBehaviour {
	Transform playerTransform = null;
	[Tooltip("Moves distance to player * speed * Time.deltaTime each frame")]
	public float speed = 0.5f;
	[Tooltip("The distance at which the camera snaps to the player position")]
	public float snapDistance = 0.2f;

	void Update() {
		UpdateTrackedPlayer();
		Follow();
	}
	void Follow() {
		if (playerTransform == null) return;
		Vector2 delta = playerTransform.position.To2D() - transform.position.To2D();
		if (delta.magnitude <= snapDistance) transform.Translate(delta);
		else transform.Translate(delta * speed);
	}
	void UpdateTrackedPlayer() {
		RespawnManager manager = RespawnManager.Instance;
		Player currentPlayer;
		if ((manager = RespawnManager.Instance) != null && (currentPlayer = manager.CurrentPlayer) != null) playerTransform = currentPlayer.transform;
	}
}
