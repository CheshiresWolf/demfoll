using UnityEngine;
using System.Collections;
using System;

public class Profile {

	public string name;
	public string avatar;

	public int daysAlive;

	public Profile() {
		this.name = "";
		this.avatar = "";
		this.daysAlive = 0;
	}

	public Profile(string _name, string _avatar) {
		this.name = _name;
		this.avatar = _avatar;
		this.daysAlive = 0;
	}

	public string convertToJson() {
		return JsonUtility.ToJson(new SerializableProfile(this));
	}

	public void readFromJson(string jsonRepresentation) {
		SerializableProfile sp = JsonUtility.FromJson<SerializableProfile>(jsonRepresentation);

		this.name = sp.name;
		this.avatar = sp.avatar;

		this.daysAlive = sp.daysAlive;
	}

	[Serializable]
	class SerializableProfile {
		public string name;
		public string avatar;

		public int daysAlive;

		public SerializableProfile(Profile profile) {
			this.name = profile.name;
			this.avatar = profile.avatar;

			this.daysAlive = profile.daysAlive;
		}
	}
}
