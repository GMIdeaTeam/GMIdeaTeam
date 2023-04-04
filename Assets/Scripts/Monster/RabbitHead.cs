using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Monster;
using UnityEngine.Pool;

public class RabbitHead : Monster
{
    private IObjectPool<RabbitHead> _rabbitHeadPool;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FollowPlayer();
            // 공격하기
        }
    }

    public void SetRabbitHeadPool(IObjectPool<RabbitHead> pool)
    {
        _rabbitHeadPool = pool;
    }

    public void DestroyRabbitHead()
    {
        _rabbitHeadPool.Release(this);
    }
}
