using UnityEngine;
using System.Collections.Generic;
using Generator;
using UnityEngine.UI;

public class TeamPanelController : MonoBehaviour {
    const float ROW_HEIGHT = 360.0f;

    float x_pos;
    float y_pos;

    float width;
    float height;

    GameObject topParent;
    GameObject prefab;
    Font ArialFont;

    List<GameObject> panels = new List<GameObject>();

    Dictionary<string, Color32> teamColors;

    // Use this for initialization
    void Start () {
        prefab = GameObject.Find("TeamInfoPrefab");
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        topParent = GameObject.Find("TeamsScroll");

        RectTransform rectTransform = (RectTransform) prefab.transform;
        width = rectTransform.rect.width;

        x_pos = this.gameObject.transform.position.x;
        y_pos = 0; // -ROW_HEIGHT / 2;

        teamColors = new Dictionary<string, Color32>() {
            { "Банда",      new Color32(255, 255, 255, 200) },
            { "Загін",      new Color32( 68, 115, 255, 200) },
            { "Група",      new Color32(255, 195, 105, 200) },
            { "Товариство", new Color32( 76, 165,  59, 200) },
            { "Коло",       new Color32( 54, 255, 230, 200) }
        };

        topParent.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void activate(bool state) {
        topParent.SetActive(state);
    }

    private void setText(Text textObject, string text) {
        textObject.text = text;

        textObject.font = ArialFont;
        textObject.material = ArialFont.material;
        textObject.color = Color.black;

        textObject.alignment = TextAnchor.MiddleCenter;
    }

    public void drawTeam(Team team) {
        GameObject newPanel = Instantiate(prefab, new Vector2(x_pos, y_pos), this.transform.rotation) as GameObject;

        newPanel.transform.SetParent(this.gameObject.transform);

        y_pos -= ROW_HEIGHT;

        setText(newPanel.transform.FindChild("Text").gameObject.AddComponent<Text>(), team.convertToPanel());

        int count = team.persons.Count;
        for (int i = 0; i < count; i++) {
            GameObject teamPerson = newPanel.transform.FindChild("TeamPerson" + i).gameObject;

            setText(teamPerson.transform.FindChild("Text").gameObject.AddComponent<Text>(), team.persons[i].convertToPanel());
            newPanel.transform.FindChild("Background").gameObject.GetComponent<Image>().color = teamColors[team.type];
        }
        
        panels.Add(newPanel);
    }

    public void drawTeams(Team[] teams) {
        foreach (GameObject item in panels) {
            Destroy(item);
        }

        panels.Clear();
        x_pos = this.gameObject.transform.position.x + width / 2;
        y_pos = this.gameObject.transform.position.y - ROW_HEIGHT / 2;

        foreach (Team item in teams) {
            drawTeam(item);
        }
    }
}
