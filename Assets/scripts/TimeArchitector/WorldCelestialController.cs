using UnityEngine;

using AbstractButterflyClass;
using System;

using System.Collections;
using System.Collections.Generic;

public class WorldCelestialController : ButterflyEffect {
    const bool DEBUG = false;

    GameObject sun;
    GameObject moon;

    GameObject celestialPanel;

    Vector2 pivot;
    Vector3 direction;
    
    void Start () {
        sun = GameObject.Find("Sun");
        moon = GameObject.Find("Moon");

        celestialPanel = GameObject.Find("World_celestials");

        RectTransform rt = (RectTransform) celestialPanel.transform;
        pivot = new Vector2(celestialPanel.transform.position.x, celestialPanel.transform.position.y - rt.rect.height / 2);

        direction = new Vector3(0, 0, 1);

        GameObject.Find("TimeArchitector").GetComponent<TimeArchitector>().addScript(this);
    }

    /**
     * Angle Step Conseption
     * Pros : simple, easy to controll floating ticks_in_day
     * Cons : twitching animation, float roundings
     */

    float MAX_ANGLE_STEP = 3.6f;

    float rotationLeft = 0.0f;

    void Update() {
        if (rotationLeft > 0.0f) {
            float msPassed = Time.deltaTime * 1000;
            float MIN_ANGLE_STEP = MAX_ANGLE_STEP * msPassed / 100; // step must invoke in ~100ms

            if (DEBUG) Debug.Log("WorldCelestialController | Update | rotationLeft : " + rotationLeft);
            if (DEBUG) Debug.Log("WorldCelestialController | Update | MIN_ANGLE_STEP : " + MIN_ANGLE_STEP);

            sun.transform.RotateAround(pivot, direction, MIN_ANGLE_STEP);
            moon.transform.RotateAround(pivot, direction, MIN_ANGLE_STEP);

            rotationLeft -= MIN_ANGLE_STEP;
        }
    }

    public override void step(int ticks_in_day) {
        MAX_ANGLE_STEP = 360.0f / ticks_in_day;
        
        rotationLeft += MAX_ANGLE_STEP;

        if (DEBUG) Debug.Log("WorldCelestialController | step | rotationLeft : " + rotationLeft);
    }

    public override void day_step(int ticks_in_day) {}

    public override void month_step(int ticks_in_day) {}
}
