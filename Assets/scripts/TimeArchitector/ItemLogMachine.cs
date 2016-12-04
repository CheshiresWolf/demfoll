using UnityEngine;
using System.Collections;
using Generator;
using System.Collections.Generic;

using UnityEngine.UI;

public class ItemLogMachine : MonoBehaviour {

	GameObject ItemLogPanel;

	GameObject prefab;
	GameObject parentPanel;
    Font ArialFont;
    
    List<GameObject> panels = new List<GameObject>();

	string fullText = "";

	// Use this for initialization
	void Start () {
		ItemLogPanel = GameObject.Find("ItemLogPanel");
		prefab = GameObject.Find("PersonPanelPrefab");
		parentPanel = GameObject.Find("ItemPanelContent");

        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
	}

	void removeItem(GameObject gm) {
		panels.Remove(gm);
		Destroy(gm);
	}
	
	public void addPerson(Person p) {
		GameObject newPanel = Instantiate(prefab, parentPanel.transform) as GameObject;

        // newPanel.transform.SetParent(parentPanel.transform);

        Text personText = newPanel.transform.FindChild("ItemText").gameObject.GetComponent<Text>();
        personText.text = p.convertToPanel();

        Button personButton = newPanel.transform.FindChild("ItemButton").gameObject.GetComponent<Button>();
    	personButton.onClick.AddListener(delegate () { this.removeItem(newPanel); });

        Debug.Log("personText : " + personText.text);
        
        personText.font = ArialFont;
        personText.material = ArialFont.material;
        personText.color = Color.black;

        personText.alignment = TextAnchor.MiddleCenter;

        panels.Add(newPanel);
	}
}