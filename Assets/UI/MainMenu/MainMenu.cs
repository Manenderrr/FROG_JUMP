using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
	public int PlayScene = 1;

	public string ButtonsContainerName = "buttons";
	public string PlayButtonText = "Play";
	public string ExitButtonText = "Exit";

	void OnEnable() {
		UIDocument document = GetComponent<UIDocument>();
		VisualElement buttonContainer = document.rootVisualElement.Q(ButtonsContainerName);

		Button playButton = new() { text = PlayButtonText };
		playButton.clicked += Play;
		buttonContainer.Add(playButton);

		Button exitButton = new() { text = ExitButtonText };
		buttonContainer.Add(exitButton);
		exitButton.clicked += Application.Quit;
	}
	void Play() {
		SceneManager.LoadScene(PlayScene);
	}
}
