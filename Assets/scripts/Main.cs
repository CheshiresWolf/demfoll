using UnityEngine;
using Generator;
using UnityEngine.UI;
using System.Collections.Generic;
using Utils;

public class Main : MonoBehaviour {
    PersonsGenerator gen;

    PersonPanelController personPanel;
    Button generateButton;

    Person[] persons;

    FileWorker fileWorker;

    // Use this for initialization
    void Start () {
        generateButton = GameObject.Find("GenerateButton").GetComponent<Button>();
        personPanel = (PersonPanelController)GameObject.Find("PersonPanel").GetComponent("PersonPanelController");

        gen = new PersonsGenerator();

        fileWorker = new FileWorker();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void startGeneration() {
        generateButton.interactable = false;

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        Debug.Log(Screen.width);
        Debug.Log(Screen.height);

        persons = gen.run(200);
        personPanel.drawPersons(persons);

        Debug.Log(sw.ElapsedMilliseconds + " ms");
        sw.Stop();

        generateButton.interactable = true;
    }

    public void savePersons() {
        // Debug.Log(persons[0].convertToJson());

        string res = "[";
        for (int i = 0; i < persons.Length; i++) {
            res += persons[i].convertToJson();

            if (i != persons.Length - 1) {
                Debug.Log("i : " + i);
                res += ", ";
            }
        }
        res += "]";

        Debug.Log(res);
        fileWorker.writeFile("tempFile.txt", res);
    }

    public void loadPersons() {
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

        Debug.Log(res);
    }
}