using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {
    public enum ClassType {
        Vagabond,
        Ranger,
        Warlock
    }
    public static int xp = 0;
    public static int currentLevel = 1;
    public static ClassType playerClass;
    public static float speed = 5.0f;
    public static int health = 100;
    public static float damage = 8.0f;
    public static float action_speed = 10.0f;
    public static float action_range = 2.0f;

}
