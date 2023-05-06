using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
  public AIPath aiPath;

    // Keeps enemmy facing player
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(-2f, 2f, 2f);
        } else if (aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(2f, 2f, 2f);
        }

    }
}
