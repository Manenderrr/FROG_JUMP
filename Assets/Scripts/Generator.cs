using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour {
	public SpriteRenderer leverSprite;
	public Sprite pulledLeverSprite;
	public float secondsBeforeEndScreen = 3;
	public int endScreenSceneIndex = 9;

	IEnumerator EndSequence() {
		leverSprite.sprite = pulledLeverSprite;
		yield return new WaitForSeconds(secondsBeforeEndScreen);
		SceneManager.LoadScene(endScreenSceneIndex);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Player>() != null) StartCoroutine(EndSequence());
	}
}
