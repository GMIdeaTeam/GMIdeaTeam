using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Util;

namespace Idea.Player
{
    public class PlayerData : MonoBehaviour
    {
        [field: SerializeField]
        public float MoveSpeed { get; set; } = 5f; // ModeController의 speed를 참조할 수 있도록 추후 변경
        public float HealthPoint { get; set; } = 100.0f;

        [HideInInspector] public Direction direction = Direction.DOWN;
        
        // flags
        [HideInInspector] public bool isMovingStage = false;
        [HideInInspector] public bool isAttacking = false;

        void Update()
        {
            //Debug.Log($"HP : {HP}");
        }
    }
}