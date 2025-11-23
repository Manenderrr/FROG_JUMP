using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {
	public Transform currentCheckpoint;
	public static GameController Instance = null;
	public Player playerPrefab;
	///<summary>Used to store the currently alive player</summary>
	public Player CurrentPlayer { get; private set; } = null;
	public bool respawnPlayerOnStart = true;
	public DeathScreen deathScreen;
	
	[Header("Blocking input")]
	public bool blockInputOnRespawn = false;
	public bool blockInputOnce = true;

	[Header("Audio")]
	public AudioSource musicSource;
	public AudioSource globalSoundSource;
	public AudioClip levelMusic;
	public AudioClip levelMusicIntro;
	public AudioClip deathMusic;
	public AudioClip deathSound;
	IEnumerator musicLoop;

	void Start() {
		if (Instance != null) {
			Debug.LogError("Attempted to create more than one GameController", this);
			Destroy(gameObject);
			return;
		}
		Instance = this;

		currentCheckpoint = transform;
		if (respawnPlayerOnStart) Respawn();
	}
	void Update() {
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
	}
	public void Death() {
		if (deathScreen == null) {
			Debug.LogError("No death screen, cannot die");
			return;
		}
		globalSoundSource.PlayOneShot(deathSound);
		SetMusicLoop(null, deathMusic);
		deathScreen.ShowDocument();
		Destroy(CurrentPlayer.gameObject);
	}
	
	public void Respawn() {
		if (CurrentPlayer != null) return;
		if (playerPrefab == null) {
			Debug.LogWarning("No player prefab assigned to GameController");
			return;
		}
		Player newPlayer = Instantiate(playerPrefab, currentCheckpoint.position, currentCheckpoint.rotation);
		CurrentPlayer = newPlayer;
		if (blockInputOnRespawn) {
			newPlayer.blockInput = true;
			if (blockInputOnce) blockInputOnRespawn = false;
		}

		deathScreen.HideDocument();
		SetMusicLoop(levelMusicIntro, levelMusic);
		
	}
	void SetMusicLoop(IEnumerator loop) {
		if (musicLoop != null) StopCoroutine(musicLoop);
		
		musicLoop = loop;
		if (loop != null) StartCoroutine(loop);
	}
	void SetMusicLoop(AudioClip introClip, AudioClip mainClip) => SetMusicLoop(PlayMusicLoop(introClip, mainClip));
	IEnumerator PlayMusicLoop(AudioClip introClip, AudioClip mainClip) {
		musicSource.Stop();
		if (introClip != null) {
			musicSource.clip = introClip;
			musicSource.Play();
			while (musicSource.isPlaying) {
				yield return null;
			}
		}
		if (mainClip != null) {
			musicSource.clip = mainClip;
			while (true) {
				musicSource.Play();
				while (musicSource.isPlaying) {
					yield return null;
				}
			}
		}
	}
}