using UnityEngine;
using System.Collections;

using AbstractButterflyClass;
using System;

public class WorldCelestialController : ButterflyEffect {
    GameObject sun;
    GameObject moon;

    GameObject celestialPanel;

    Vector2 pivot;

    // Use this for initialization
    void Start () {
        sun = GameObject.Find("Sun");
        moon = GameObject.Find("Moon");

        celestialPanel = GameObject.Find("World_celestials");

        GameObject.Find("TimeArchitector").GetComponent<TimeArchitector>().addScript(this);

        RectTransform rt = (RectTransform) celestialPanel.transform;
        // pivot = new Vector2(celestialPanel.transform.position.x + rt.rect.width / 2, celestialPanel.transform.position.y + rt.rect.height / 2);
        pivot = new Vector2(celestialPanel.transform.position.x, celestialPanel.transform.position.y);

        Debug.Log("celestialPanel.transform.position.x : " + celestialPanel.transform.position.x);
        Debug.Log("celestialPanel.transform.position.y : " + celestialPanel.transform.position.y);
        Debug.Log("rt.rect.width : " + rt.rect.width);
        Debug.Log("rt.rect.height : " + rt.rect.height);

        Debug.Log("pivot : " + pivot);
    }

    public override void step() {
        Debug.Log("Time.time : " + Time.time);

        sun.transform.RotateAround(pivot, new Vector3(0, 0, 1), 3.6f);
        moon.transform.RotateAround(pivot, new Vector3(0, 0, 1), 3.6f);
    }
}
