using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
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
	
	State state = State.Sitting;
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
			foreach (Collider2D collider in colliders) {
				print(collider.name);
			}
			print($"IsOnGround check: {amount > 0}");
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

		TeleportBool();
	}

	void Update() {
		if (state == State.Sitting) SittingUpdate();

		VisualForceJump();

		if (teleport == true) {
			Teleported();
		}
	}

	void SittingUpdate() {
		if (Input.GetKeyDown(KeyCode.Mouse0) && state == State.Sitting) {
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
		if (!IsOnGround) Die();
	}
	public void Die() {
		if (deathScreen == null) {
			Debug.LogError("No death screen, cannot die");
			return;
		}
		if (GlobalSound.Instance != null && deathClip != null ) GlobalSound.Instance.PlayOneShot(deathClip);
		Instantiate(deathScreen);
		Destroy(gameObject);
	}
	
	void VisualForceJump() {
<<<<<<< HEAD
		slider.value = _startSlider;
	}

	[Header("Teleporting")]
	public LayerMask teleporter;
	public bool teleport;
	public int toScene;

	void TeleportBool() {
		teleport = Physics2D.OverlapCircle(transform.position, 0.4f, teleporter);
	}

	public void Teleported() {
		SceneManager.LoadScene(toScene);
=======
		if (slider != null) slider.value = _startSlider;
>>>>>>> d2abc6d6fd80ab0cfc0926549a91ceed9f733d5d
	}
}