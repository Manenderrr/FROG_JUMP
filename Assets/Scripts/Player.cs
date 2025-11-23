using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[Header("Input")]
	public bool blockInput = false;

	[Header("Rigidbody 2D")]
	[SerializeField] Rigidbody2D _rb;

	[Header("Spinning")]
	[Tooltip("Rotation speed in degrees per second")]
	public float RotationSpeed = 50;

	[Header("Jumping")]
	[Tooltip("Jump distance in Unity distance units")]
	public float jumpDistance = 1;
	[Tooltip("Jump time in seconds")]
	public float jumpTime;
	public float jumpForce;
	
	public State state = State.Sitting;
	public enum State {
		Sitting, Jumping, PreparingForJump
	}
	
	[Header("Sounds")]
	public AudioSource audioSource;
	public AudioClip jumpClip;
	public AudioClip deathClip;

	[Header("Death by overhydration")]
	public LayerMask ground;
	public Collider2D groundContactCollider;

	bool IsOnGround {
		get {
			
			List<Collider2D> colliders = new();
			int amount = groundContactCollider.GetContacts(new ContactFilter2D() { layerMask = ground, useLayerMask = true, useTriggers = true }, colliders);
			return amount > 0;
		}
	}
	public DeathScreen deathScreen;

	[Header("Sprites")]
	[SerializeField] SpriteRenderer sprite;
	public Sprite frogJump;
	public Sprite frogSit;

	[Header("Slider")]
	public Slider slider;
	private float _startSlider = 5;

	private void Start() {
		sprite.sprite = frogSit;

		if (slider != null) slider.gameObject.SetActive(false);
	}

	void FixedUpdate() {
		if (state == State.Sitting) _rb.rotation += RotationSpeed * Time.deltaTime;
	}

	void Update() {
		if (state == State.Sitting) SittingUpdate();

		VisualForceJump();
	}

	void SittingUpdate() {
		if (!blockInput && Input.GetKeyDown(KeyCode.Mouse0) && state == State.Sitting) {
			StartCoroutine(Jump());
		}
	}

	IEnumerator Jump() {
		state = State.PreparingForJump;

		while (Input.GetKey(KeyCode.Mouse0)) {
			if (slider != null) slider.gameObject.SetActive(true);
			
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
		if (slider != null) slider.gameObject.SetActive(false);
		_rb.linearVelocity = _rb.GetRelativeVector(Vector2.up) * jumpDistance / jumpTime * (jumpForce + 1);
		sprite.sprite = frogJump;
		if (audioSource != null && jumpClip != null) audioSource.PlayOneShot(jumpClip);

		yield return new WaitForSeconds(jumpTime);

		Sitting();
		
		DieIfNotOnGround();
	}

	void Sitting() {
		_rb.linearVelocity = Vector2.zero;
		sprite.sprite = frogSit;
		jumpTime = 0.2f;
		jumpForce = 0;
		_startSlider = 5;
		state = State.Sitting;
	}

	void DieIfNotOnGround() {
		if (!IsOnGround) GameController.Instance.Death();
	}
	
	void VisualForceJump() {
		if (slider != null) slider.value = _startSlider;
	}
}