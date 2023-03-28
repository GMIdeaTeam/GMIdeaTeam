using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Monster;
using UnityEngine.PlayerLoop;

public class Rabbit : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        Wander();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hor, ver);
    }
}
