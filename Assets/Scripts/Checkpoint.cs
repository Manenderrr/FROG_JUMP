using UnityEngine;

public class Checkpoint : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		Player player = other.GetComponent<Player>();
		if (player != null && RespawnManager.Instance != null) RespawnManager.Instance.currentCheckpoint = transform;
	}
}
