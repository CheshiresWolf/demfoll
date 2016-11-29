using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manners {
    List<string> manners = new List<string>() { "Грубість", "Дипломатія", "Таємничість", "Гумор", "Відкритість", "Маніпуляція" };

    public void apply(Player player, string mannerName) {
        if (manners.Contains(mannerName)) {
            player.manners.Add(mannerName);
        }
    }
}
