using UnityEngine;
using System.Collections;

using AbstractButterflyClass;
using UnityEngine.UI;

public class TimeFlowController : ButterflyEffect {
    const bool DEBUG = false;

    Text label;

    int currentDay = 0;
    int local_ticks_in_day = 100; // 10 ticks in second, 10 seconds in day

    string speedMultiplier = "1x";

	void Start () {
        label = GameObject.Find("Time_text").GetComponent<Text>();

        GameObject.Find("TimeArchitector").GetComponent<TimeArchitector>().addScript(this);
    }
	
	void Update () {
	
	}

    int stepsCount = 0;

    public override void step(int ticks_in_day) {
        if (local_ticks_in_day != ticks_in_day) {
            if (DEBUG) Debug.Log("TimeFlowController | step | stepsCount : " + stepsCount);
            float flowMultiplier = (float) ticks_in_day / local_ticks_in_day;
            if (DEBUG) Debug.Log("TimeFlowController | step | flowMultiplier : " + flowMultiplier);
            stepsCount = (int) (stepsCount * flowMultiplier); // loosing steps at this moment, need to be checked (e.g. 91 * 0.33 = 30.03 to int 30)
            if (DEBUG) Debug.Log("TimeFlowController | step | stepsCount : " + stepsCount);

            switch (ticks_in_day) {
                case 50 :
                    speedMultiplier = "3x";
                    break;
                case 100 :
                    speedMultiplier = "1x";
                    break;
                case 300 :
                    speedMultiplier = "0.5x";
                    break;
            }

            local_ticks_in_day = ticks_in_day;

            updateText();
        }

        if (stepsCount < local_ticks_in_day) {
            stepsCount += 1;
        } else {
            stepsCount = 0;
            currentDay += 1;

            updateText();
        }
    }

    private void updateText() {
        label.text = "Day : " + currentDay + ", Speed : " + speedMultiplier + ".";
    }
}
