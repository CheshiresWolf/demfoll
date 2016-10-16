using UnityEngine;
using Generator;
using UnityEngine.UI;
using System.Collections.Generic;
using Utils;

public class Main : MonoBehaviour {
    PersonsGenerator personGenerator;
    TeamGenerator teamGenerator;

    PersonPanelController personPanel;
    TeamPanelController teamPanel;

    Button generateButton;
    Button loadButton;
    Button saveButton;
    Button generateTeamsButton;

    Person[] persons;
    Team[] teams;

    FileWorker fileWorker;

    const int PERSONS_AMOUNT = 200;
    const int TEAMS_AMOUNT = 15;

    // Use this for initialization
    void Start () {
        generateButton = GameObject.Find("GenerateButton").GetComponent<Button>();
        loadButton = GameObject.Find("LoadButton").GetComponent<Button>();
        saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        generateTeamsButton = GameObject.Find("GenerateTeamsButton").GetComponent<Button>();

        saveButton.gameObject.SetActive(false);
        generateTeamsButton.gameObject.SetActive(false);
        
        personPanel = (PersonPanelController)GameObject.Find("PersonPanel").GetComponent("PersonPanelController");
        teamPanel = (TeamPanelController)GameObject.Find("TeamPanel").GetComponent("TeamPanelController");

        personGenerator = new PersonsGenerator();
        teamGenerator = new TeamGenerator();

        fileWorker = new FileWorker();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void setInteractable(bool state) {
    	generateButton.interactable = state;
	    loadButton.interactable = state;
	    saveButton.interactable = state;
	    generateTeamsButton.interactable = state;
    }

    private void showPersonsTab() {
        personPanel.activate(true);
        teamPanel.activate(false);
    }

    private void showTeamsTab() {
        personPanel.activate(false);
        teamPanel.activate(true);
    }

    // generate/load buttons active by default,
    // other two depends on them and must be activated later
    bool levelTwoActivate = false;
    private void activateButtons() {
        if (levelTwoActivate) return;

        saveButton.gameObject.SetActive(true);
        generateTeamsButton.gameObject.SetActive(true);
    }

    public void startGeneration() {
        setInteractable(false);

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        persons = personGenerator.run(PERSONS_AMOUNT);
        showPersonsTab();
        personPanel.drawPersons(persons);

        Debug.Log("Persons generation time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();

        activateButtons();
        setInteractable(true);
    }

    public void savePersons() {
        setInteractable(false);

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        string res = "[";
        for (int i = 0; i < persons.Length; i++) {
            res += persons[i].convertToJson();

            if (i != persons.Length - 1) {
                res += ", ";
            }
        }
        res += "]";

        fileWorker.writeFile("tempFile.txt", res);

        Debug.Log("Persons save time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();

        setInteractable(true);
    }

    public void loadPersons() {
        setInteractable(false);

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        string res = fileWorker.readFile("tempFile.txt");

        res = res.Substring(1, res.Length - 2);
        string[] jsonsArray = res.Split(new string[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries);

        persons = new Person[jsonsArray.Length];

        for (int i = 0; i < jsonsArray.Length; i ++) {
            Debug.Log(jsonsArray[i]);

            persons[i] = new Person();
            persons[i].readFromJson(jsonsArray[i]);
        }

        personPanel.drawPersons(persons);

        Debug.Log("Persons load time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();

        activateButtons();
        setInteractable(true);
    }

    private void generateTeams(bool generateOthers) {
        setInteractable(false);

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        teams = teamGenerator.run(persons, TEAMS_AMOUNT, generateOthers);
        showTeamsTab();
        teamPanel.drawTeams(teams);

        Debug.Log("Teams generation time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();

        setInteractable(true);
    }

    public void emptyTeamGeneration() {
        generateTeams(false);
    }

    public void defaultTeamGeneration() {
        generateTeams(true);
    }
}