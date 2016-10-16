using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
	public class Team {
        public int id;
        public string name = "<mighty team name>";
        public string type;
        public string baseBiography;
        public int prestige;
        public int money;
        public string state;

        public string history;

        public List<string> property = new List<string>();

        public List<Person> persons = new List<Person>();
        public List<string> perks = new List<string>(); // "Особливості", maybe some team specific perks

        public void addPerson(Person person) {
            // check person before add
            persons.Add(person);
        }

        // =======<Converters>=======

        public string convertToJson() {
            return JsonUtility.ToJson(new SerializableTeam(this));
        }

        public void readFromJson(string jsonRepresentation) {
            
        }

        public string convertToPanel() {
            return id + ", " + name + ", " + type + ", " + baseBiography;
        }
    }

    [Serializable]
    class SerializableTeam {
        public int id;
        public string name;
        public string type;
        public string baseBiography;
        public int prestige;
        public int money;
        public string state;
        public string history;

        public List<string> property;
        public List<string> persons;
        public List<string> perks;

        public SerializableTeam(Team team) {
            this.id = team.id;
            this.name = team.name;
            this.type = team.type;
            this.baseBiography = team.baseBiography;
            this.prestige = team.prestige;
            this.money = team.money;
            this.state = team.state;
            this.history = team.history;

            this.property = team.property;
            this.perks = team.perks;

            this.persons = new List<string>();
            foreach (Person person in team.persons) {
            	this.persons.Add(person.convertToJson());
            }
        }
    }
}