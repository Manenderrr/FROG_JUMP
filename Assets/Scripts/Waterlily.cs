using UnityEngine;

public class Waterlily : MonoBehaviour {
	private void Start() {
		float rand;
		rand = Random.Range(0, 360f);
		transform.rotation = Quaternion.Euler(0f, 0f, rand);
	}
}