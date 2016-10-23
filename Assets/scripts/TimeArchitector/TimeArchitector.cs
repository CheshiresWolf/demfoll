using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AbstractButterflyClass;
using UnityEngine.UI;

public class TimeArchitector : MonoBehaviour {
    private float ms_in_tick = 100.0f; // 10 tick in second
    private ButterflyEffectController butterfly;

    private int[] ticks_variants = new int[] { 50, 100, 300 };
    private int ticks_index = 1; // default 100 - 10 ticks in second, 10 seconds in day

    private bool isActive = true;

    Text label;

    void Start () {
        label = GameObject.Find("Time_pause/Text").GetComponent<Text>();

        butterfly = new ButterflyEffectController();
	}
	
	void Update () {
        if (isActive && theTimeHasCome()) {
            butterfly.step(ticks_variants[ticks_index]);
        }
	}

    public void addScript(ButterflyEffect script) {
        butterfly.add(script);
    }

    public void removeScript(ButterflyEffect script) {
        butterfly.remove(script);
    }

    public void speedUp() {
        if (ticks_index < 2) ticks_index++;
    }

    public void slowDown() {
        if (ticks_index > 0) ticks_index--;
    }

    public void pause() {
        isActive = !isActive;

        label.text = isActive ? "Pause" : "Start";
    }

    private float lastTickMeasure = 0.0f;
    private bool theTimeHasCome() {
        float currentTime = Time.time * 1000;

        if ( (currentTime - lastTickMeasure) > ms_in_tick ) {
            lastTickMeasure = currentTime;

            return true;
        }

        return false;
    }
}

class ButterflyEffectController {
    private List<ButterflyEffect> queue = new List<ButterflyEffect>();

    public void step(int ticks_in_day) {
        foreach (ButterflyEffect script in queue) {
            script.step(ticks_in_day);
        }
    }

    public void add(ButterflyEffect script) {
        queue.Add(script);
    }

    public void remove(ButterflyEffect script) {
        
    }
}