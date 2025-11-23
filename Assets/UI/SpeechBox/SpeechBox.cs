using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeechBox : MonoBehaviour {
	[SerializeField] UIDocument ui;
	TextElement _text;
	[SerializeField] string textName = "text";
	
	public List<string> lines;
	[Tooltip("Caution: starts from zero!")]
	public int currentLineIndex = 0;
	
	void OnEnable() {
		_text = ui.rootVisualElement.Q<TextElement>(textName);
		_text.text = lines[currentLineIndex];
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			currentLineIndex++;
			UpdateDisplay();
		}
	}
	
	void UpdateDisplay() {
		if (currentLineIndex >= lines.Count) {
			Destroy(gameObject); // We are done
			return;
		}
		if (currentLineIndex < 0) {
			Debug.LogError($"currentLineIndex is lesser than 0 {currentLineIndex}"); // wut
			return;
		}
		_text.text = lines[currentLineIndex];
	}
	
	void HideDocument() {
		ui.rootVisualElement.style.display = DisplayStyle.None;
	}
	void ShowDocument() {
		ui.rootVisualElement.style.display = StyleKeyword.Null;
	}
}