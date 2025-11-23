using UnityEngine;

public class SmoothFollowingCamera : MonoBehaviour {
	Transform playerTransform = null;
	[Tooltip("Movement speed in Unity units per second")]
	public float speed = 3;

	void Update() {
		UpdateTrackedPlayer();
		Follow();
	}
	void Follow() {
		if (playerTransform == null) return;
		Vector2 playerPosition = playerTransform.position;
		Vector2 delta = playerPosition - transform.position.To2D();
		if (delta.magnitude <= speed * Time.deltaTime) transform.position = new(playerPosition.x, playerPosition.y, transform.position.z); // We are fast enough to teleport to the player in this frame, but we don't want to change z
		else transform.Translate(delta.normalized * speed * Time.deltaTime);
	}
	void UpdateTrackedPlayer() {
		GameController manager = GameController.Instance;
		Player currentPlayer;
		if ((manager = GameController.Instance) != null && (currentPlayer = manager.CurrentPlayer) != null) playerTransform = currentPlayer.transform;
	}
}
