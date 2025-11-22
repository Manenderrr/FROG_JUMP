using System.Collections;
using UnityEngine;

public class DistanceMeasurer : MonoBehaviour {
	public new Rigidbody2D rigidbody2D;
	public float push = 1;
	public float time = 1;

	Vector2 initialPos;

	void Start() {
		print($"Measuring for {time} seconds with velocity {1}");
		initialPos = transform.position;
		StartCoroutine(WaitAndPrintResult());
	}
	void FixedUpdate() {
		rigidbody2D.linearVelocity = Vector2.up * push;
	}
	IEnumerator WaitAndPrintResult() {
		yield return new WaitForSeconds(time);
		Vector2 travelled = transform.position.To2D() - initialPos;
		print($"Travelled {travelled} in {time}s, average speed {travelled.magnitude / time} u/s");
	}
}