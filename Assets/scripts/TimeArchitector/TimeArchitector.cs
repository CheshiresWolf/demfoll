using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AbstractButterflyClass;

public class TimeArchitector : MonoBehaviour {
    private float ms_in_tick = 100.0f; // 10 tick in second
    private ButterflyEffectController butterfly;

	// Use this for initialization
	void Start () {
        butterfly = new ButterflyEffectController();
	}
	
	// Update is called once per frame
	void Update () {
        if (theTimeHasCome()) {
            butterfly.step();
        }
	}

    public void addScript(ButterflyEffect script) {
        butterfly.add(script);
    }

    public void removeScript(ButterflyEffect script) {
        butterfly.remove(script);
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

    public void step() {
        foreach (ButterflyEffect script in queue) {
            script.step();
        }
    }

    public void add(ButterflyEffect script) {
        queue.Add(script);
    }

    public void remove(ButterflyEffect script) {
        
    }
}