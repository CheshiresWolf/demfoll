using UnityEngine;
using System.Collections;
using Generator;
using System;
using System.Collections.Generic;

public abstract class AbstractOrigin {
    public abstract void apply(Player player);
}

public class FarColonies : AbstractOrigin {
    public override void apply(Player player) {
        player.stats.HLTH += 50;
        player.stats.CRFT += 50;
    }
}

public class NobleHouse : AbstractOrigin {
    public override void apply(Player player) {
        player.money += 50;
    }
}


public class Origin{

    Dictionary<string, AbstractOrigin> origins = new Dictionary<string, AbstractOrigin> {
        { "Далекі Колонії",         new FarColonies()           },
        { "Шляхетний дім",          new NobleHouse()            },
    };

    public void apply(Player player, string originName) {
        origins[originName].apply(player);
        player.origin.Add(originName);
    }
}
