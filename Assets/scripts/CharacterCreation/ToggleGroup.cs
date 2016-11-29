using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ToggleGroup {

	GameObject parent;
	public GameObject[] toggles;
	public bool enabled = true;

	public ToggleGroup(GameObject obj, GameObject[] togglesArray) {
		this.parent = obj;
		this.toggles = togglesArray;
	}

	public void show() {
		parent.SetActive(true);
	}

	public void hide() {
		parent.SetActive(false);
	}

    public int countToggled() {
        int check = 0;
        foreach (GameObject toggleObject in toggles) {
            Toggle t = toggleObject.GetComponent<Toggle>();
            if (t && t.isOn) {
                check++;
            }
        }
        return check;
    }

    public int[] getToggled() {
    	List<int> list = new List<int>();
    	int i = 0;
	    foreach (GameObject toggleObject in toggles) {
            Toggle t = toggleObject.GetComponent<Toggle>();
            if (t && t.isOn) {
                list.Add(i);
            }
            i++;
        }
        return list.ToArray();
    }

    public void enableToggles() {
    	if (enabled) return;
        foreach (GameObject toggleObject in toggles) {
            Toggle t = toggleObject.GetComponent<Toggle>();
            if (t) {
                t.interactable = true;
            }
        }
        enabled = true;
    }

    public void disableToggles() {
    	if (!enabled) return;
    	foreach (GameObject toggleObject in toggles) {
            Toggle t = toggleObject.GetComponent<Toggle>();
            if (t && !t.isOn) {
                t.interactable = false;
            }
        }
        enabled = false;   
    }

    public void toggleOff() {
		foreach (GameObject toggleObject in toggles) {
            Toggle t = toggleObject.GetComponent<Toggle>();
            if (t) {
                t.isOn = false;
            }
        }
    } 
}
