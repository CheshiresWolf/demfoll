using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AbstractButterflyClass;
using UnityEngine.UI;

using Generator;
using Utils;

public class TimeArchitector : MonoBehaviour {
    private float ms_in_tick = 100.0f; // 10 tick in second
    private ButterflyEffectController butterfly;

    private int[] ticks_variants = new int[] { 50, 100, 300 };
    private int ticks_index = 1; // default 100 - 10 ticks in second, 10 seconds in day

    private bool isActive = false;

    Text label;

    PersonsGenerator personGenerator;
    TeamGenerator teamGenerator;

    public Person[] persons;
    public Team[] teams;

    const int PERSONS_AMOUNT = 200;
    const int TEAMS_AMOUNT = 15;

    LogMachine log;

    void Start () {
        log = GameObject.Find("LogText").GetComponent<LogMachine>();
        log.addText("Demfoll is awake. Starting generation.");

        personGenerator = new PersonsGenerator();
        teamGenerator = new TeamGenerator();

        label = GameObject.Find("Time_pause/Text").GetComponent<Text>();

        butterfly = new ButterflyEffectController();

        startGeneration();

        addScript(new WorldEvents(this));

        log.addText("The Place Where Time Begins...");
        isActive = true;
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

    public void startGeneration() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        persons = personGenerator.run(PERSONS_AMOUNT);
        log.addText(PERSONS_AMOUNT + " sitizens was generated in " + sw.ElapsedMilliseconds + " ms");

        teams = teamGenerator.run(persons, TEAMS_AMOUNT, true);
        log.addText(TEAMS_AMOUNT + " teams was generated in " + sw.ElapsedMilliseconds + " ms");

        sw.Stop();
    }
}