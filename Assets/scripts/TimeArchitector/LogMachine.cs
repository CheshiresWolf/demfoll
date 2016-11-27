using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class LogMachine : MonoBehaviour {

	GameObject logPanel;
	Text textPanel;

	string fullText = "";

	// Use this for initialization
	void Start () {
		logPanel = GameObject.Find("LogText");
		textPanel = logPanel.GetComponent<Text>();
	}
	
	public void addText(string text) {
		fullText += text + " \n";
		textPanel.text = fullText;
	}
}
