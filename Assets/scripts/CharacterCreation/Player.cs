using System;
using System.Collections;
using Generator;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person {
    //public int id;
    //public string name;
    //public string surname;
    //public int money;
    //public string currentState;
    //public string location;
    //public int health = 12;

    //public List<string> biography = new List<string>();
    //public List<string> perks = new List<string>();
    new public int money = 0;
    new public List<string> origin = new List<string>();
    new public List<string> biography = new List<string>();
    new public List<string> mentor = new List<string>();
    new public List<string> perks = new List<string>();
    new public List<string> manners = new List<string>();
    public int focus = 0;

    new public Stats stats = new Stats(120);
    //public Inventory inventory = new Inventory();
    public Property property = new Property();

    public class Property {

        public string convertToJson() {
            return "[]";
        }

        public void readFromList(List<string> list) {

        }

        public List<string> toList() {
            return new List<string>();
        }
    }

    // =======<Checks>=======

    public bool isMentorExists(string mentorName) {
        return this.mentor.Contains(mentorName);
    }

    // =======<Converters>=======
    public string convertToJson() {
        return JsonUtility.ToJson(new SerializablePlayer(this));
    }

    public void readFromJson(string jsonRepresentation) {
        SerializablePlayer sp = JsonUtility.FromJson<SerializablePlayer>(jsonRepresentation);

        this.id = sp.id;
        this.name = sp.name;
        this.surname = sp.surname;
        this.money = sp.money;
        this.currentState = sp.currentState;
        this.location = sp.location;
        this.health = sp.health;
        this.focus = sp.focus;

        this.origin = sp.origin;
        this.biography = sp.biography;
        this.perks = sp.perks;
        this.mentor = sp.mentor;
        this.manners = sp.manners;

        this.stats = new Stats(120);
        this.stats.readFromList(sp.stats);

        this.inventory = new Inventory();
        this.inventory.readFromList(sp.inventory);

        this.property = new Property();
        this.property.readFromList(sp.property);
    }

    public string convertToPanel() {
        string statsString = "MLC : " + stats.MLC +
            ", RNGC : " + stats.RNGC +
            ", HLTH : " + stats.HLTH +
            ", ATHL : " + stats.ATHL +
            ", CONSP : " + stats.CONSP +
            ", STLTH : " + stats.STLTH +
            ", INVST : " + stats.INVST +
            ",\nCMRC : " + stats.CMRC +
            ", INT : " + stats.INT +
            ", MYST : " + stats.MYST +
            ", CHAR : " + stats.CHAR +
            ", CRFT : " + stats.CRFT +
            ", WLP : " + stats.WLP;

        return id + ", " + name + " " + surname + ", " +
            money + " марок, " +
            "биографии : [" + string.Join(", ", biography.ToArray()) + "], " +
            "менторы : [" + string.Join(", ", mentor.ToArray()) + "], " +
            "перки : [" + string.Join(",", perks.ToArray()) + "], " +
            "\n" + statsString;
    }

    [Serializable]
    class SerializablePlayer {
        public int id;
        public string name;
        public string surname;
        public int money;
        public string currentState;
        public string location;
        public int health;
        public int focus;

        public List<string> origin;
        public List<string> biography;
        public List<string> mentor;
        public List<string> perks;
        public List<string> manners;
        public List<int> stats;
        public List<string> inventory;
        public List<string> property;

        public SerializablePlayer(Player player) {
            this.id = player.id;
            this.name = player.name;
            this.surname = player.surname;
            this.currentState = player.currentState;
            this.location = player.location;
            this.health = player.health;

            this.origin = player.origin;
            this.biography = player.biography;
            this.mentor = player.mentor;
            this.perks = player.perks;
            this.manners = player.manners;

            this.stats = player.stats.toList();
            this.inventory = player.inventory.toList();
            this.property = player.property.toList();
        }
    }
}
