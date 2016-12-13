using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
    public class Stats {
        public int MLC = 10; // Ближній Бій;
        public int RNGC = 10; // Дальній Бій;
        public int HLTH = 10; // Тілобудова;
        public int ATHL = 10; // Атлетика;
        public int CONSP = 10; // Конспірація(соціальна);
        public int STLTH = 10; // Скритність(фізична);
        public int INVST = 10; // Розслідування;
        public int CMRC = 10; // Комерція;
        public int INT = 10; // Інтелект;
        public int MYST = 10; // Містичні знання;
        public int CHAR = 10; // Харизма;
        public int CRFT = 10; // Ремесло;
        public int WLP = 10; // Сила Волі;

        public Stats(int defaultVal) {
            this.MLC = defaultVal;
            this.RNGC = defaultVal;
            this.HLTH = defaultVal;
            this.ATHL = defaultVal;
            this.CONSP = defaultVal;
            this.STLTH = defaultVal;
            this.INVST = defaultVal;
            this.CMRC = defaultVal;
            this.INT = defaultVal;
            this.MYST = defaultVal;
            this.CHAR = defaultVal;
            this.CRFT = defaultVal;
            this.WLP = defaultVal;
        }
        public string convertToJson() {
            return "[" + MLC + ", " + RNGC + ", " + HLTH + ", " + ATHL + ", " + CONSP + ", " + STLTH + ", " + INVST + ", " + CMRC + ", " + INT + ", " + MYST + ", " + CHAR + ", " + CRFT + ", " + WLP + "]";
        }

        public void readFromList(List<int> list) {
            this.MLC   = list[0];
            this.RNGC  = list[1];
            this.HLTH  = list[2];
            this.ATHL  = list[3];
            this.CONSP = list[4];
            this.STLTH = list[5];
            this.INVST = list[6];
            this.CMRC  = list[7];
            this.INT   = list[8];
            this.MYST  = list[9];
            this.CHAR  = list[10];
            this.CRFT  = list[11];
            this.WLP   = list[12];
        }

        public List<int> toList() {
            return new List<int>() {
                MLC, RNGC, HLTH, ATHL, CONSP, STLTH, INVST, CMRC, INT, MYST, CHAR, CRFT, WLP
            };
        }
    }

    public class Inventory {

        public string convertToJson() {
            return "[]";
        }

        public void readFromList(List<string> list) {

        }

        public List<string> toList() {
            return new List<string>();
        }
    }

    public class Person {
        public int id;
        public string name;
        public string surname;
        public string gender;
        public string nation;
        public int age;
        public string nature; // character|pattern|spirit
        public int money;
        public string profession; // maybe this it need to be moved in externall class
        public string currentState;
        public string location;
        public int health = 12;
        public string morality;
        public string motivation;
        public string history;
        public int prestige;
        public int loyality; // relation with character

        public List<string> biography = new List<string>();
        public List<string> perks = new List<string>();

        public Stats stats = new Stats(10);
        public Inventory inventory = new Inventory();

        public int teamId;

        // =======<Checks>=======

        public bool isBiographyExists(string biographyName) {
            return this.biography.Contains(biographyName);
        }

        // =======<Converters>=======

        public string convertToJson() {
            return JsonUtility.ToJson(new SerializablePerson(this));
        }

        public void readFromJson(string jsonRepresentation) {
            SerializablePerson sp = JsonUtility.FromJson<SerializablePerson>(jsonRepresentation);

            this.id = sp.id;
            this.name = sp.name;
            this.surname = sp.surname;
            this.gender = sp.gender;
            this.nation = sp.nation;
            this.age = sp.age;
            this.nature = sp.nature;
            this.money = sp.money;
            this.profession = sp.profession;
            this.currentState = sp.currentState;
            this.location = sp.location;
            this.health = sp.health;
            this.morality = sp.morality;
            this.motivation = sp.motivation;
            this.history = sp.history;
            this.prestige = sp.prestige;
            this.loyality = sp.loyality;

            this.biography = sp.biography;
            this.perks = sp.perks;

            this.stats = new Stats(10);
            this.stats.readFromList(sp.stats);

            this.inventory = new Inventory();
            this.inventory.readFromList(sp.inventory);

            this.teamId = sp.teamId;
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
                gender + ", " +
                nation + ", " +
                age + " лет, " +
                nature + ", " +
                money + " марок, " +
                "биографии : [" + string.Join(", ", biography.ToArray()) + "], " +
                "перки : [" + string.Join(",", perks.ToArray()) + "], " +
                "\n" + statsString;
        }
    }

    [Serializable]
    class SerializablePerson {
        public int id;
        public string name;
        public string surname;
        public string gender;
        public string nation;
        public int age;
        public string nature;
        public int money;
        public string profession;
        public string currentState;
        public string location;
        public int health;
        public string morality;
        public string motivation;
        public string history;
        public int prestige;
        public int loyality;

        public List<string> biography;
        public List<string> perks;
        public List<int> stats;
        public List<string> inventory;

        public int teamId;

        public SerializablePerson(Person person) {
            this.id = person.id;
            this.name = person.name;
            this.surname = person.surname;
            this.gender = person.gender;
            this.nation = person.nation;
            this.age = person.age;
            this.nature = person.nature;
            this.money = person.money;
            this.profession = person.profession;
            this.currentState = person.currentState;
            this.location = person.location;
            this.health = person.health;
            this.morality = person.morality;
            this.motivation = person.motivation;
            this.history = person.history;
            this.prestige = person.prestige;
            this.loyality = person.loyality;

            this.biography = person.biography;
            this.perks = person.perks;

            this.stats = person.stats.toList();
            this.inventory = person.inventory.toList();

            this.teamId = person.teamId;
        }
    }
}