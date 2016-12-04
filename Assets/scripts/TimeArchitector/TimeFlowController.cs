using UnityEngine;
using System.Collections;

using AbstractButterflyClass;
using UnityEngine.UI;

using Utils;

public class TimeFlowController : ButterflyEffect {
    const bool DEBUG = false;

    Text label;

    int currentDay = 0;
    int local_ticks_in_day = 100; // 10 ticks in second, 10 seconds in day

    string speedMultiplier = "1x";

    LogMachine log;
    RandomUtils utils;

    string[] timePhrases = new string[7] {
        " aaaaand nothing hapens.",
        " sity is boring as always.",
        " mighty circus, with clowns, trained animals and bearded women visit some other town.",
        " does elephant suspects that we looking on him?",
        " i think i see bird flying arowd elephant neck! And it's gone.",
        " 8-800...",
        " it's not oblivious, but it looks like sun is in deeeep depression and slowly dying inside... hey bird is back!"
    };
    int[] timePhrasesProbs = new int[7] {58, 15, 7, 7, 7, 3, 3};

	void Start () {
        utils = new RandomUtils();

        label = GameObject.Find("Time_text").GetComponent<Text>();

        GameObject.Find("TimeArchitector").GetComponent<TimeArchitector>().addScript(this);

        log = GameObject.Find("LogText").GetComponent<LogMachine>();
    }
	
	void Update () {
	
	}

    int stepsCount = 0;

    public override void step(int ticks_in_day) {
        if (local_ticks_in_day != ticks_in_day) {
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

            updateText();
        }
    }

    public override void day_step(int ticks_in_day) {
        currentDay += 1;

        log.addText("Day " + currentDay + "" + utils.getRandomFromArrays(timePhrases,timePhrasesProbs));

        updateText();
    }

    public override void month_step(int ticks_in_day) {}

    private void updateText() {
        label.text = "Day : " + currentDay + ", Speed : " + speedMultiplier + ".";
    }
}
