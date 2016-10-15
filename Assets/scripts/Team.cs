using System.Collections.Generic;
using UnityEngine;

namespace Generator {
	public class Team {
        public string type;
        public string baseBiography;

        public List<Person> persons = new List<Person>();
        public List<string> perks = new List<string>();

        public void addPerson(Person person) {
            // check person before add
            persons.Add(person);
        }
    }
}