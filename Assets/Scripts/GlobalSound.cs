using UnityEngine;

public class GlobalSound : MonoBehaviour {
	public static AudioSource Instance;
	public AudioClip death;
	private GameObject player;
	
	void Awake() {
		if (Instance != null) {
			Debug.LogError("Attempting to create multiple GlobalSound instances");
		}
		AudioSource source = GetComponent<AudioSource>();
		if (source != null) Instance = source;

		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update() {
		if (player == null) {
			Instance.PlayOneShot(death);
		}
	}
}
