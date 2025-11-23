using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathScreen : MonoBehaviour {
	[SerializeField] string restartButtonName = "restart-button";
	
	void OnEnable() {
		UIDocument ui = GetComponent<UIDocument>();
		Button restart = ui.rootVisualElement.Q<Button>(restartButtonName);

		restart.clicked += Restart;
	}
	public static void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
