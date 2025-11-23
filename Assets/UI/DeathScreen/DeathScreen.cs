using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathScreen : MonoBehaviour {
	[SerializeField] string restartButtonName = "restart-button";
	
	void OnEnable() {
		UIDocument ui = GetComponent<UIDocument>();
		Button restart = ui.rootVisualElement.Q<Button>(restartButtonName);

		restart.clicked += CloseAndRestart;
	}
	public static void RestartByReload() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public static void RestartByRespawn() {
		if (RespawnManager.Instance == null) {
			Debug.LogError("No respawn manager");
			return;
		}
		RespawnManager.Instance.Respawn();
	}
	public void CloseAndRestart() {
		RestartByRespawn();
		Destroy(gameObject);
	}
}