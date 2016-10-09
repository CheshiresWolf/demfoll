using UnityEngine;
using System.Collections;

using Generator;
using UnityEngine.UI;
using System.Collections.Generic;

public class PersonPanelController : MonoBehaviour {
    const float ROW_HEIGHT = 60.0f;

    float x_pos = 50;
    float y_pos = 50;

    float width;
    float height;

    GameObject prefab;
    Font ArialFont;

    List<GameObject> panels = new List<GameObject>();

    // Use this for initialization
    void Start () {
        prefab = GameObject.Find("PersonInfoPrefab");
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        RectTransform rectTransform = (RectTransform) prefab.transform;
        width = rectTransform.rect.width;

        x_pos = this.gameObject.transform.position.x;
        y_pos = 0; // -ROW_HEIGHT / 2;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void drawPerson(Person person) {
        GameObject newPanel = Instantiate(prefab, new Vector2(x_pos, y_pos), this.transform.rotation) as GameObject;

        newPanel.transform.SetParent(this.gameObject.transform);

        y_pos -= ROW_HEIGHT;

        Text personText = newPanel.transform.FindChild("Text").gameObject.AddComponent<Text>();
        personText.text = person.convertToPanel();
        
        personText.font = ArialFont;
        personText.material = ArialFont.material;
        personText.color = Color.black;

        personText.alignment = TextAnchor.MiddleCenter;

        panels.Add(newPanel);
    }

    public void drawPersons(Person[] persons) {
        foreach (GameObject item in panels) {
            Destroy(item);
        }

        panels.Clear();
        x_pos = this.gameObject.transform.position.x + width / 2;
        y_pos = -10 + this.gameObject.transform.position.y;

        foreach (Person item in persons) {
            drawPerson(item);
        }
    }
}
