using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Monster;

public class RabbitBody : Monster
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FollowPlayer();
            // 공격하기
        }
    }
}
