using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
	[SerializeField] string exitButtonName = "exit";
	UIDocument ui;
	
	void Awake() {
		ui = GetComponent<UIDocument>();
	}
	void OnEnable() {
		Button exit = ui.rootVisualElement.Q<Button>(exitButtonName);

		exit.clicked += Application.Quit;
	}
}
