using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[Header("Rigidbody 2D")]
	[Tooltip("Rigidbody 2D")]
	[SerializeField] private Rigidbody2D _rb;

	[Header("Spinning")]
	[Tooltip("Rotation speed in degrees per second")]
	public float RotationSpeed = 50;

	[Header("Jumping")]
	[Tooltip("Jump distance Unity distance units")]
	public float jumpDistance = 1;
	[Tooltip("Jump time in seconds")]
	public float jumpTime = 1;
	public float jumpForce;

	[Header("Sprites")]
	[SerializeField] private SpriteRenderer _frog;
	public Sprite frogJump;
	public Sprite frogSit;

	[Header("Slider")]
	public Slider slider;


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

		VisualForceJump();
	}

	void SittingUpdate() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			StartCoroutine(Jump());
		}
	}

	IEnumerator Jump() {
		state = State.PreparingForJump;

		while (Input.GetKey(KeyCode.Mouse0)) {
			jumpForce += Time.deltaTime * 2;

			if (jumpForce > 5) {
				jumpForce = 5;
			}
			yield return null;
		}

		state = State.Jumping;
		_rb.linearVelocity = _rb.GetRelativeVector(Vector2.up) * jumpDistance / jumpTime * (jumpForce + 1);
		_frog.sprite = frogJump;

		yield return new WaitForSeconds(jumpTime);

		_rb.linearVelocity = Vector2.zero;
		_frog.sprite = frogSit;
		jumpForce = 0;
		state = State.Sitting;
	}

	void VisualForceJump()
	{
		slider.value = jumpForce;
		slider.maxValue = 5;
	}

}