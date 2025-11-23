using UnityEngine;

public class RespawnManager : MonoBehaviour {
	public Transform currentCheckpoint;
	public static RespawnManager Instance = null;
	public Player playerPrefab;
	///<summary>Used to store the currently alive player</summary>
	public Player CurrentPlayer { get; private set; } = null;
	public bool respawnPlayerOnStart = true;

	void Start() {
		if (Instance != null) {
			Debug.LogError("Attempted to create more than one RespawnManager", this);
			Destroy(gameObject);
			return;
		}
		Instance = this;

		currentCheckpoint = transform;
		Respawn();
	}

	public void Respawn() {
		if (CurrentPlayer != null) return;
		if (playerPrefab == null) {
			Debug.LogWarning("No player prefab assigned to RespawnManager");
			return;
		}
		Player newPlayer = Instantiate(playerPrefab, currentCheckpoint.position, currentCheckpoint.rotation);
		CurrentPlayer = newPlayer;
	}
}