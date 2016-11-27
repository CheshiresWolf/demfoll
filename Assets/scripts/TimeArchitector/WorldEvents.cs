using System.Collections.Generic;
using UnityEngine;

using Generator;
using Utils;
using AbstractButterflyClass;

abstract class AbstractEvent {
	public int offset;
	public int counter;

	public float probability;

	/**
	 * 0 - activate event if (counter > offset)
	 * 1 - event activation based of random generator
	 * 2 - activated if (counter > offset) and random renerator check passed
	 * 3 - auto activate on check
	 * 4+ - event activation turned off
	 */
	public int activationType;

    public abstract void run(Person[] newPersons, Team[] newTeams);
}

class MortisEvent : AbstractEvent {
	private RandomUtils utils;
	private LogMachine log;

	public MortisEvent(RandomUtils utils, LogMachine log) {
		this.offset = 1;
		this.activationType = 0;

		this.utils = utils;
		this.log = log;
	}

    public override void run(Person[] newPersons, Team[] newTeams) {
    	int cursedIndex = utils.getRandomBetween(0, newPersons.Length);
    	Person[] bufPersons = new Person[newPersons.Length - 1];
    	Person doomed = null;

    	int bufIndex = 0;
    	for (int i = 0; i < newPersons.Length; i++) {
    		if (i == cursedIndex) {
    			doomed = newPersons[i];
    		} else {
    			bufPersons[bufIndex] = newPersons[i];
    			bufIndex++;
    		}
    	}

    	for (int i = 0; i < newTeams.Length; i++) {
    		if (newTeams[i].id == doomed.teamId) {
    			newTeams[i].removePerson(doomed);
    			break;
    		}
    	}

    	newPersons = bufPersons;

    	Debug.Log("WorldEvents | MortisEvent | run");

    	log.addText("Щасти і На Все Добре!\nПерсоною, на ім'я " + doomed.name + " " + doomed.surname + ", було знайдено щастя у життя. " + doomed.name + " " + doomed.surname + " більше не займається сумнівними справами.");
    }
}

class VitaemEvent : AbstractEvent {
	private RandomUtils utils;
	private LogMachine log;

	public VitaemEvent(RandomUtils utils, LogMachine log) {
		this.offset = 20;
		this.activationType = 0;

		this.utils = utils;
		this.log = log;
	}

    public override void run(Person[] newPersons, Team[] newTeams) {
    	Debug.Log("WorldEvents | VitaemEvent | run");
    }
}

public class WorldEvents : ButterflyEffect {
	private List<AbstractEvent> stepEvents;
	private List<AbstractEvent> dayEvents;
	private List<AbstractEvent> monthEvents;

	private TimeArchitector timeArchitector;

    private LogMachine log = GameObject.Find("LogText").GetComponent<LogMachine>();
    private RandomUtils utils = new RandomUtils();

    public WorldEvents(TimeArchitector ta) {
    	this.timeArchitector = ta;

    	this.stepEvents = new List<AbstractEvent> ();
		this.dayEvents = new List<AbstractEvent> {
			new MortisEvent(utils, log),
			new VitaemEvent(utils, log)
		};
		this.monthEvents = new List<AbstractEvent> ();
    }

    public override void step(int ticks_in_day) {}

    public override void day_step(int ticks_in_day) {
    	Debug.Log("WorldEvents | day_step");

    	foreach (AbstractEvent ae in dayEvents) {
            check(ae);
        }
    }

    public override void month_step(int ticks_in_day) {}

    void check(AbstractEvent ae) {
    	switch (ae.activationType) {
    		case 0 :
    			if (ae.counter >= ae.offset) {
    				ae.counter = 0;
    				ae.run(this.timeArchitector.persons, this.timeArchitector.teams);
    			} else {
    				ae.counter++;
    			}

    			break;
    		case 1 :
    			if (utils.getRandomBetween(0, 100) < ae.probability) {
    				ae.run(this.timeArchitector.persons, this.timeArchitector.teams);
    			}
    			
    			break;
    		case 2 :
    			if (ae.counter >= ae.offset) {
    				ae.counter = 0;

    				if (utils.getRandomBetween(0, 100) < ae.probability) {
	    				ae.run(this.timeArchitector.persons, this.timeArchitector.teams);
	    			}
    			} else {
    				ae.counter++;
    			}

    			break;
    		case 3 :
    			ae.run(this.timeArchitector.persons, this.timeArchitector.teams);

    			break;
    	}
    }
}
