using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Monster;
using UnityEngine.Pool;
using DG.Tweening;

public class RabbitBody : Monster
{
    private const int RABBIT_HEAD_CNT = 8;
    public RabbitHead rabbitHeadPrefab;
    private IObjectPool<RabbitHead> _pool;

    public Ease ease;

    private Vector3[] wayPoint = new Vector3[]
    {
        // 1
        new Vector3(0, 0, 0),
        new Vector3(-3.5f, 1f, 0),
        new Vector3(-4f, 0f, 0),
        // 2
        new Vector3(0, 0, 0),
        new Vector3(-2.5f, 1f, 0),
        new Vector3(-3f, 0f, 0),
        // 3
        new Vector3(0, 0, 0),
        new Vector3(-1.5f, 1f, 0),
        new Vector3(-2f, 0f, 0),
        // 4
        new Vector3(0, 0, 0),
        new Vector3(-0.5f, 1f, 0),
        new Vector3(-1f, 0f, 0),
        // 5
        new Vector3(0, 0, 0),
        new Vector3(0.5f, 1f, 0),
        new Vector3(1f, 0f, 0),
        // 6
        new Vector3(0, 0, 0),
        new Vector3(1.5f, 1f, 0),
        new Vector3(2f, 0f, 0),
        // 7
        new Vector3(0, 0, 0),
        new Vector3(2.5f, 1f, 0),
        new Vector3(3f, 0f, 0),
        // 8
        new Vector3(0, 0, 0),
        new Vector3(3.5f, 1f, 0),
        new Vector3(4f, 0f, 0),
    };

    private Vector3[] wayPoints;

    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    private void Awake()
    {
        _pool = new ObjectPool<RabbitHead>(CreateRabbitHead, OnGetRabbitHead, OnReleaseRabbitHead, OnDestroyRabbitHead, maxSize:RABBIT_HEAD_CNT);
    }

    private void OnEnable()
    {
        HeadFountain();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FollowPlayer();
            // 공격하기
        }
    }
    
    void HeadFountain()
    {
        for (int i = 0; i < RABBIT_HEAD_CNT; i++)
        {
            RabbitHead rabbitHead = _pool.Get();
            
            // 분수 효과
            wayPoints = new Vector3[3];
            wayPoints.SetValue(wayPoint[3 * i], 0);
            wayPoints.SetValue(wayPoint[3 * i + 1], 1);
            wayPoints.SetValue(wayPoint[3 * i + 2], 2);


            rabbitHead.transform.DOPath(wayPoints, 6.0f, PathType.CatmullRom).SetLookAt(new Vector3(0f, 0f, 0f)).SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    RabbitHead CreateRabbitHead()
    {
        RabbitHead rabbitHead = Instantiate(rabbitHeadPrefab).GetComponent<RabbitHead>();
        rabbitHead.SetRabbitHeadPool(_pool);

        return rabbitHead;
    }

    void OnGetRabbitHead(RabbitHead rabbitHead)
    {
        rabbitHead.gameObject.SetActive(true);
    }
    
    void OnReleaseRabbitHead(RabbitHead rabbitHead)
    {
        rabbitHead.gameObject.SetActive(false);
    }

    void OnDestroyRabbitHead(RabbitHead rabbitHead)
    {
        Destroy(rabbitHead.gameObject);
    }
}
