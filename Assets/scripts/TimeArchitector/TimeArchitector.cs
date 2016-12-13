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

    //Text label;

    PersonsGenerator personGenerator;
    TeamGenerator teamGenerator;

    public Person[] persons;
    public Team[] teams;

    const int PERSONS_AMOUNT = 200;
    const int TEAMS_AMOUNT = 15;

    LogMachine log;

    FileWorker fileWorker;

    string playerName;
    Player player;


    void Start () {
        log = GameObject.Find("LogText").GetComponent<LogMachine>();
        log.addText("Demfoll is awake. Starting generation.");

        fileWorker = new FileWorker();

        playerName = PlayerPrefs.GetString("Player");
        if (playerName.Length > 0) {
            Debug.Log("Player: " + playerName);
            loadPlayerData();
        }

        personGenerator = new PersonsGenerator();
        teamGenerator = new TeamGenerator();

        //label = GameObject.Find("Time_pause/Text").GetComponent<Text>();

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

        //label.text = isActive ? "Pause" : "Start";
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

    void loadPlayerData() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        string res = fileWorker.readFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + playerName + "\\" + "player.txt");

        if (res.Length > 0) {
            Debug.Log(res);
            player = new Player();
            player.readFromJson(res);

            loadPersons();
            if (persons.Length > 0) log.addText("Persons were loaded from player " + playerName + " data in " + sw.ElapsedMilliseconds + " ms");

            loadTeams();
            if (teams.Length > 0) log.addText("Teams were loaded from player " + playerName + " data in " + sw.ElapsedMilliseconds + " ms");
        }

        sw.Stop();
    }

    void loadPersons() {
        string res = fileWorker.readFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + playerName + "\\" + "persons.txt");

        if (res.Length <= 0) {
            persons = new Person[0];
            return;
        }
        res = res.Substring(1, res.Length - 2);
        string[] jsonsArray = res.Split(new string[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries);

        persons = new Person[jsonsArray.Length];

        for (int i = 0; i < jsonsArray.Length; i ++) {
            //Debug.Log(jsonsArray[i]);

            persons[i] = new Person();
            persons[i].readFromJson(jsonsArray[i]);
        }
    }

    void loadTeams() {
        string res = fileWorker.readFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + playerName + "\\" + "persons.txt");

        if (res.Length <= 0) {
            teams = new Team[0];
            return;
        }
        res = res.Substring(1, res.Length - 2);
        string[] jsonsArray = res.Split(new string[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries);

        teams = new Team[jsonsArray.Length];

        for (int i = 0; i < jsonsArray.Length; i ++) {
            //Debug.Log(jsonsArray[i]);

            teams[i] = new Team();
            teams[i].readFromJson(jsonsArray[i]);
        }
    }

    void savePlayer() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        fileWorker.writeFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + playerName + "\\" + "player.txt", player.convertToJson());

        Debug.Log("Player save time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();
    }

    void savePersons() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        string res = "[";
        for (int i = 0; i < persons.Length; i++) {
            res += persons[i].convertToJson();

            if (i != persons.Length - 1) {
                res += ", ";
            }
        }
        res += "]";

        fileWorker.writeFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + playerName + "\\" + "persons.txt", res);

        Debug.Log("Persons save time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();
    }

    void saveTeams() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        string res = "[";
        for (int i = 0; i < teams.Length; i++) {
            res += teams[i].convertToJson();

            if (i != teams.Length - 1) {
                res += ", ";
            }
        }
        res += "]";

        fileWorker.writeFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + playerName + "\\" + "teams.txt", res);

        Debug.Log("Teams save time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();
    }


    public void startGeneration() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        if (persons.Length == 0) {
            persons = personGenerator.run(PERSONS_AMOUNT);
            savePersons();
            log.addText(PERSONS_AMOUNT + " sitizens was generated in " + sw.ElapsedMilliseconds + " ms");
        }
        

        if (teams.Length == 0) {
            teams = teamGenerator.run(persons, TEAMS_AMOUNT, true);
            saveTeams();
            log.addText(TEAMS_AMOUNT + " teams was generated in " + sw.ElapsedMilliseconds + " ms"); 
        }
        
        sw.Stop();
    }
}