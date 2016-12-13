using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

    Button continueBtn;

	// Use this for initialization
	void Start () {
        string currentPlayer = PlayerPrefs.GetString("Player");
        continueBtn = GameObject.Find("ContinueBtn").GetComponent<Button>();
        continueBtn.gameObject.SetActive(false);
        PlayerPrefs.SetString("Player", null);
        if (currentPlayer.Length > 0) {
            continueBtn.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void startCampaign() {
        PlayerPrefs.SetString("Player", "SomeName");
    }

    public void exit() {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit ();
    #endif
    }
}
