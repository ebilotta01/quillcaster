using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceHandler {
    public void addExperience(int amount) {
        Player.xp += amount;
    }
    public void calculateLevel() {
        Player.level = (Player.xp - 1)/4;
    }
}