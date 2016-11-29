using UnityEngine;
using System.Collections;

public class ProfileMessage {

	public Profile profile;
	public int index;

	public ProfileMessage() {
		this.profile = new Profile();
		this.index = 1;
	}

	public ProfileMessage(Profile p, int index) {
		this.profile = p;
		this.index = index;
	}
}
