using UnityEngine;

public class GlobalSound : MonoBehaviour {
	public static AudioSource Instance;
	
	void Awake() {
		if (Instance != null) {
			Debug.LogError("Attempting to create multiple GlobalSound instances");
		}
		AudioSource source = GetComponent<AudioSource>();
		if (source != null) Instance = source;
	}
}
