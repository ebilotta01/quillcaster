using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player {
    public static float speed = 5.0f;
    public static int health = 100;
    public static float damage = 8.0f;
    public static int xp = 0;
    public static int level = 0;
    public enum player_class {
        Vagabond,
        Ranger,
        Warlock
    }
}
