using UnityEngine;
using System.Collections;
using Generator;
using System;
using System.Collections.Generic;

public abstract class AbstractMentor {
    public abstract void apply(Player player);
}

public class TravelingKnight : AbstractMentor {
    public override void apply(Player player) {
        player.stats.MLC += 50;
    }
}

public class HereticalTheologian : AbstractMentor {
    public override void apply(Player player) {
        player.stats.WLP += 50;
    }
}

public class CharmingCourtesan : AbstractMentor {
    public override void apply(Player player) {
        player.stats.CHAR += 50; // ???
    }
}

public class PuppeteerPolitician : AbstractMentor {
    public override void apply(Player player) {
        player.stats.INVST += 50;
    }
}

public class RetiredSpy : AbstractMentor {
    public override void apply(Player player) {
        player.stats.CONSP += 50;
    }
}


public class Mentor {

    Dictionary<string, AbstractMentor> mentors = new Dictionary<string, AbstractMentor> {
        { "Мандрівний Лицар",       new TravelingKnight()           },
        { "Єретичний Богослов",     new HereticalTheologian()       },
        { "Чаруюча Куртизанка",     new CharmingCourtesan()         },
        { "Політик Кукловод",       new PuppeteerPolitician()       },
        { "Відставний Шпигун",      new RetiredSpy()                }
    };

    public void apply(Player player, string mentorName) {
        mentors[mentorName].apply(player);
        player.mentor.Add(mentorName);
    }
}
