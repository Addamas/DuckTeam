using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : EnemyBase
{

    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        //  ScriptedEvent(start.position, end.position, false, true);               start position, end position, can see the player, teleport back to the original place    //
    }

    public void Update()
    {
        Movement();
    }
    
}
