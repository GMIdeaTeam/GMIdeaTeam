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
        public float HP { get; set; } = 100.0f;

        [HideInInspector]
        public Direction direction = Direction.DOWN;

        public Rigidbody2D PlayerRigidbody2D { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log($"HP : {HP}");
        }
    }
}