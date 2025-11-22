using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
	[Header("Spinning")]
	[Tooltip("Rotation speed in degrees per second")]
	public float RotationSpeed = 50;
	
	[Header("Jumping")]
	[Tooltip("Jump distance Unity distance units")]
	public float JumpDistance = 1;
	[Tooltip("Jump time in seconds")]
	public float JumpTime = 1;

	[Header("Sprites")]
	[SerializeField] private SpriteRenderer _frog;
	public Sprite frogJump;
	public Sprite frogSit;

	[SerializeField] private Rigidbody2D _rb;

	State state = State.Sitting;
	public enum State {
		Sitting, Jumping, PreparingForJump
	}

	private void Start() {
		_frog.sprite = frogSit;
	}

	void FixedUpdate() {
		if (state == State.Sitting) _rb.rotation += RotationSpeed * Time.deltaTime;
	}
	void Update() {
		if (state == State.Sitting) SittingUpdate();
	}
	void SittingUpdate() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			StartCoroutine(Jump());
		}
	}
	IEnumerator Jump() {
		state = State.PreparingForJump;

		float force = 0;
		
		while (Input.GetKey(KeyCode.Mouse0)) {
			force += Time.deltaTime;
			yield return null;
		}

		state = State.Jumping;
		_rb.linearVelocity = _rb.GetRelativeVector(Vector2.up) * JumpDistance / JumpTime * (force + 1);

		_frog.sprite = frogJump;

		yield return new WaitForSeconds(JumpTime);

		_rb.linearVelocity = Vector2.zero;
		_frog.sprite = frogSit;
		state = State.Sitting;
	}
}