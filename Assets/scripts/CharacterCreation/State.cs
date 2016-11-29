using UnityEngine;
using System.Collections;

public class State {

	public string name;
	public int activeToggles;

	public State() {

		this.name = "";
		this.activeToggles = 1;
	}

	public State(string title, int num) {
		this.name = title;
		this.activeToggles = num;
	}
}
