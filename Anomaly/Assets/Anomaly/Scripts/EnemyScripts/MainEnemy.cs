using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : EnemyBase{

    
    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        Movement();
    } 


}
