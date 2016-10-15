using Generator;
using System.Collections.Generic;

public abstract class AbstractBiography {
    public abstract void apply(Person person);
}

public class SoldierBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.RNGC += 5;
        person.stats.ATHL += 3;
        person.stats.MLC  += 3;
    }
}

public class HobboBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.STLTH += 5;
        person.stats.INVST += 5;

        person.money = (person.money >= 15) ? (person.money - 15) : 0;
    }
}

public class SailorBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.ATHL += 5;
        person.stats.HLTH += 5;
    }
}

public class ColonistBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.CRFT += 5;
        person.stats.WLP += 5;
    }
}

public class StudentBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.INT += 5;
        person.stats.INVST += 5;
    }
}

public class GamblerBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.CHAR += 5;
        person.stats.INT += 3;
        person.stats.CONSP += 3;
    }
}

public class CriminalBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.MLC += 3;
        person.stats.RNGC += 3;
        person.stats.STLTH += 3;
        person.stats.ATHL += 3;
    }
}

public class WorkerBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.CRFT += 5;
        person.stats.ATHL += 5;
    }
}

public class CitizenBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.INT += 5;
        person.stats.CMRC += 5;
    }
}

public class ShadowCultistBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.INVST += 5;
        person.stats.CONSP += 5;
    }
}

public class OccultCultistBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.MYST += 5;
        person.stats.WLP += 5;
    }
}

public class NobleBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.CHAR += 5;
        person.stats.WLP += 5;
    }
}

public class BohemiaBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.INT += 5;
        person.stats.CHAR += 5;
    }
}

public class DoctorBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.INT += 5;

        person.perks.Add("Лікарь (1)");
    }
}

public class CourtesanBiography : AbstractBiography {
    public override void apply(Person person) {
        person.stats.CONSP += 5;
        person.stats.CHAR += 5;
    }
}

public class Biography {
    Dictionary<string, AbstractBiography> biographies = new Dictionary<string, AbstractBiography> {
        { "Солдат",        new SoldierBiography()       },
        { "Жебрак",        new HobboBiography()         },
        { "Моряк",         new SailorBiography()        },
        { "Колоніст",      new ColonistBiography()      },
        { "Студент",       new StudentBiography()       },
        { "Аферист",       new GamblerBiography()       },
        { "Злочинець",     new CriminalBiography()      },
        { "Робочий",       new WorkerBiography()        },
        { "Міщанин",       new CitizenBiography()       },
        { "Свідок Тінь",   new ShadowCultistBiography() },
        { "Свідок Окульт", new OccultCultistBiography() },
        { "Аристократ",    new NobleBiography()         },
        { "Богема",        new BohemiaBiography()       },
        { "Лікар",         new DoctorBiography()        },
        { "Куртизанка",    new CourtesanBiography()     },
        { "Міщанка",       new CitizenBiography()       },
        { "Робоча",        new WorkerBiography()        }
    };

    public void apply(Person person, string biographyName) {
        biographies[biographyName].apply(person);
        person.biography.Add(biographyName);
    }
}