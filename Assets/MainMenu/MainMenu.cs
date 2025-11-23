using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
	public string PlayScene = "Game";

	public string ButtonsContainerName = "buttons";
	public string PlayButtonText = "Play";
	public string ExitButtonText = "Exit";

	Button _playButton;
	Button _exitButton;

	void OnEnable() {
		UIDocument document = GetComponent<UIDocument>();
		VisualElement buttonContainer = document.rootVisualElement.Q(ButtonsContainerName);

		_playButton = CreateButton(PlayButtonText, Play);
		buttonContainer.Add(_playButton);

		_exitButton = CreateButton(ExitButtonText, Exit);
		buttonContainer.Add(_exitButton);
	}
	void OnDisable() {
		_playButton.UnregisterCallback<ClickEvent>(Play);
		_exitButton.UnregisterCallback<ClickEvent>(Exit);
	}
	
	Button CreateButton(string text, EventCallback<ClickEvent> callback) {
		Button result = new() { text = text };
		result.RegisterCallback(callback);
		return result;
	}
	void Play(ClickEvent e) {
		SceneManager.LoadScene(PlayScene);
	}
	void Exit(ClickEvent e) {
		Application.Quit();
	}
}
