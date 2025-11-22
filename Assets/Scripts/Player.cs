using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
	[Header("Spinning")]
	[Tooltip("Rotation speed in degrees per second")]
	public float rotationSpeed = 50;
	
	[Header("Jumping")]
	[Tooltip("Jump distance Unity distance units")]
	public float JumpDistance = 1;
	[Tooltip("Jump time in seconds")]
	public float JumpTime = 1;

	[Header("Components")]
	public Rigidbody2D rigidbody2D;

	State _state = State.Sitting;
	State state {
		get => _state;
		set {
			print($"{value} now");
			_state = value;
		}
	}
	public enum State {
		Sitting, Jumping, PreparingForJump
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (state == State.Sitting) rigidbody2D.rotation += rotationSpeed * Time.deltaTime;
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
		
		rigidbody2D.linearVelocity = rigidbody2D.GetRelativeVector(Vector2.up) * JumpDistance / JumpTime * (force + 1);
		state = State.Jumping;
		
		yield return new WaitForSeconds(JumpTime);

		rigidbody2D.linearVelocity = Vector2.zero;
		state = State.Sitting;
	}
}