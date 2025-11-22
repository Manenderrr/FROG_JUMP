using UnityEngine;

public class OnGround : MonoBehaviour
{
	[SerializeField] private GameObject _playerObject;
	private Player _player;

	private void Start() {
		_player = _playerObject.GetComponent<Player>();
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) _player.isOnGround = true;
	}
	void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) _player.isOnGround = false;
	}
}
