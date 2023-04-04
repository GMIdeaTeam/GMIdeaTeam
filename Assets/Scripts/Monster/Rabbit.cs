using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Monster;
using UnityEngine.PlayerLoop;

public class Rabbit : Monster
{
    [SerializeField] private GameObject RabbitBody;
    
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        Wander();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hor, ver) * MoveSpeed;
    }

    void AfterDie()
    {
        // 몸통 등장
        RabbitBody.SetActive(true);
        Invoke("HeadFountain", 0.3f);
    }
    
    void HeadFountain()
    {
        // instantiate
    }
}
