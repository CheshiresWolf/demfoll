using UnityEngine;
using System.Collections;

using AbstractButterflyClass;
using System;

public class WorldCelestialController : ButterflyEffect {
    const bool DEBUG = false;

    GameObject sun;
    GameObject moon;

    GameObject celestialPanel;

    Vector2 pivot;
    Vector3 direction;

    float MIN_ANGLE_STEP = 0.72f;
    float MAX_ANGLE_STEP = 3.6f;

    float rotationLeft = 0.0f;
    
    void Start () {
        sun = GameObject.Find("Sun");
        moon = GameObject.Find("Moon");

        celestialPanel = GameObject.Find("World_celestials");

        RectTransform rt = (RectTransform) celestialPanel.transform;
        pivot = new Vector2(celestialPanel.transform.position.x, celestialPanel.transform.position.y - rt.rect.height / 2);

        direction = new Vector3(0, 0, 1);

        GameObject.Find("TimeArchitector").GetComponent<TimeArchitector>().addScript(this);
    }

    void Update() {
        if (rotationLeft > 0.0f) {
            if (DEBUG) Debug.Log("WorldCelestialController | Update | rotationLeft : " + rotationLeft);

            sun.transform.RotateAround(pivot, direction, MIN_ANGLE_STEP);
            moon.transform.RotateAround(pivot, direction, MIN_ANGLE_STEP);

            rotationLeft -= MIN_ANGLE_STEP;
        }
    }

    public override void step(int ticks_in_day) {
        MAX_ANGLE_STEP = 360.0f / ticks_in_day;
        MIN_ANGLE_STEP = MAX_ANGLE_STEP / 5; // this 5 is wrong and need to be floating based on Time.deltaTime

        rotationLeft += MAX_ANGLE_STEP;

        if (DEBUG) Debug.Log("WorldCelestialController | step | rotationLeft : " + rotationLeft);
    }
}
