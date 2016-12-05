using UnityEngine;
using System.Collections;

public class LogPanelController : MonoBehaviour {
	GameObject leftPanelButton;
	GameObject bothPanelButton;
	GameObject rightPanelButton;

	GameObject leftPanel;
	GameObject rightPanel;

	// true - open, false - close
	bool leftPanelState = true;
	bool rightPanelState = true;

	public Sprite leftButtonImage;
	public Sprite rightButtonImage;
	public Sprite bothOnButtonImage;
	public Sprite bothOffButtonImage;

	// Use this for initialization
	void Start () {
		leftPanelButton = GameObject.Find("LeftPanelButton");
		bothPanelButton = GameObject.Find("BothPanelButton");
		rightPanelButton = GameObject.Find("RightPanelButton");

		leftPanel = GameObject.Find("LogView");
		rightPanel = GameObject.Find("ItemLogPanel");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleLeftPanel() {
		leftPanelState = !leftPanelState;
		leftPanel.GetComponent<CanvasRenderer>().CrossFadeAlpha(
			leftPanelState ? 1.0f : 0.0f,
			1.0f,
			false
		);

		leftPanelButton.GetComponent<Image>().sprite = leftPanelState ? leftButtonImage : rightButtonImage;
	}

	public void toggleBothPanels() {
		if (leftPanelState && rightPanelState) {
			bothPanelButton.GetComponent<Image>().sprite = bothOffButtonImage;
		} else {
			leftPanelState = false;
			rightPanelState = false;

			bothPanelButton.GetComponent<Image>().sprite = bothOnButtonImage;
		}

		toggleLeftPanel();
		toggleRightPanel();
	}

	public void toggleRightPanel() {
		rightPanelState = !rightPanelState;
		rightPanel.GetComponent<CanvasRenderer>().CrossFadeAlpha(
			rightPanelState ? 1.0f : 0.0f,
			1.0f,
			false
		);

		rightPanelButton.GetComponent<Image>().sprite = rightPanelState ? rightButtonImage : leftButtonImage;
	}
}
