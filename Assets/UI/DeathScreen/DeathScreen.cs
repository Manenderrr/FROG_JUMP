using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class DeathScreen : MonoBehaviour {
	[SerializeField] string restartButtonName = "restart-button";
	UIDocument ui;

	public void HideDocument() {
		ui.rootVisualElement.style.display = DisplayStyle.None;
	}
	public void ShowDocument() {
		ui.rootVisualElement.style.display = StyleKeyword.Null;
	}
	
	void Awake() {
		ui = GetComponent<UIDocument>();
	}
	void OnEnable() {
		Button restart = ui.rootVisualElement.Q<Button>(restartButtonName);

		restart.clicked += CloseAndRestart;
	}
	public static void RestartByRespawn() {
		if (GameController.Instance == null) {
			Debug.LogError("No respawn manager");
			return;
		}
		GameController.Instance.Respawn();
	}
	public void CloseAndRestart() {
		RestartByRespawn();
	}
}