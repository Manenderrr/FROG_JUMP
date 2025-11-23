using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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
	public float jumpTime;
	public float jumpForce;

	[Header("Sprites")]
	[SerializeField] private SpriteRenderer _frog;
	public Sprite frogJump;
	public Sprite frogSit;

	[Header("Slider")]
	public Slider slider;
	private float _startSlider = 5;

	public bool isOnGround = true;
	public LayerMask ground;

	State state = State.Sitting;

	public enum State {
		Sitting, Jumping, PreparingForJump
	}

	private void Start() {
		_frog.sprite = frogSit;

		slider.gameObject.SetActive(false);
	}

	void FixedUpdate() {
		if (state == State.Sitting) _rb.rotation += RotationSpeed * Time.deltaTime;
	}

	void Update() {
		if (state == State.Sitting) SittingUpdate();

		VisualForceJump();

		CheckGround();
	}

	void SittingUpdate() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			StartCoroutine(Jump());
		}
	}

	IEnumerator Jump() {
		state = State.PreparingForJump;

		while (Input.GetKey(KeyCode.Mouse0)) {
			slider.gameObject.SetActive(true);
			jumpForce += Time.deltaTime * 5;
			jumpTime += Time.deltaTime;
			_startSlider -= Time.deltaTime * 5;

			if (jumpForce > 5) {
				jumpForce = 5;
			}
			if (jumpTime > 0.75f) {
				jumpTime = 0.75f;
			}
			yield return null;
		}

		state = State.Jumping;
		slider.gameObject.SetActive(false);
		_rb.linearVelocity = _rb.GetRelativeVector(Vector2.up) * jumpDistance / jumpTime * (jumpForce + 1);
		_frog.sprite = frogJump;

		yield return new WaitForSeconds(jumpTime);

		_rb.linearVelocity = Vector2.zero;
		_frog.sprite = frogSit;
		jumpTime = 0.2f;
		jumpForce = 0;
		_startSlider = 5;
		state = State.Sitting;
		DieIfNotOnGround();
	}

	void DieIfNotOnGround() {
		if (isOnGround == false) print("you are supposed to die");
	}

	void VisualForceJump() {
		slider.value = _startSlider;
	}

	void CheckGround () {
		isOnGround = Physics2D.OverlapCircle(transform.position, 0.5f, ground);
	}
}