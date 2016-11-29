using Generator;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPerk {
    public abstract void apply(Person person);
}

public class GeniusPerk : AbstractPerk {
    public override void apply(Person person) {
    	person.stats.MLC   += 10;
        person.stats.RNGC  += 10;
        person.stats.HLTH  += 10;
        person.stats.ATHL  += 10;
        person.stats.CONSP += 10;
        person.stats.STLTH += 10;
        person.stats.INVST += 10;
        person.stats.CMRC  += 10;
        person.stats.INT   += 10;
        person.stats.MYST  += 10;
        person.stats.CHAR  += 10;
        person.stats.CRFT  += 10;
        person.stats.WLP   += 10;
    }
}

public class LooserPerk : AbstractPerk {
    public override void apply(Person person) {
    	person.stats.MLC   -= 10;
        person.stats.RNGC  -= 10;
        person.stats.HLTH  -= 10;
        person.stats.ATHL  -= 10;
        person.stats.CONSP -= 10;
        person.stats.STLTH -= 10;
        person.stats.INVST -= 10;
        person.stats.CMRC  -= 10;
        person.stats.INT   -= 10;
        person.stats.MYST  -= 10;
        person.stats.CHAR  -= 10;
        person.stats.CRFT  -= 10;
        person.stats.WLP   -= 10;
    }
}

public class YoungPerk : AbstractPerk {
    public override void apply(Person person) {
    	person.age = 15;
    }
}

public class OldPerk : AbstractPerk {
    public override void apply(Person person) {
    	person.age += 25;
    }
}

public class DoctorPerk : AbstractPerk {
    public override void apply(Person person) {
    	// O_o
    }
}

public class EmpathyPerk : AbstractPerk {
    public override void apply(Person person) {
        // O_o
    }
}

public class DiplomatPerk : AbstractPerk {
    public override void apply(Person person) {
        // O_o
    }
}

public class PlayerGeniusPerk : AbstractPerk {
    public override void apply(Person person) {
        // O_o
    }
}

public class ImmunityPerk : AbstractPerk {
    public override void apply(Person person) {
        // O_o
    }
}

public class PlayerDoctorPerk : AbstractPerk {
    public override void apply(Person person) {
        // O_o
    }
}

public class Perks {
    Dictionary<string, AbstractPerk> perks = new Dictionary<string, AbstractPerk> {
        { "Геній",      new GeniusPerk()      },
        { "Невдаха",    new LooserPerk()      },
        { "Юність",     new YoungPerk()       },
        { "Старість",   new OldPerk()         },
        { "Лікарь",     new DoctorPerk()      },
        { "Емпат",      new EmpathyPerk()             },
        { "Дипломат",   new DiplomatPerk()            },
        { "Геніальний", new PlayerGeniusPerk()        },
        { "Імунітет",   new ImmunityPerk()            },
        { "Доктор",     new PlayerDoctorPerk()        }        

    };

    public void apply(Person person, string perkName) {
        perks[perkName].apply(person);
        person.perks.Add(perkName);
    }
}