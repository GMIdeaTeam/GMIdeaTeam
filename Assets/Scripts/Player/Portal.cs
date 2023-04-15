using Idea.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Idea.Util;

namespace Idea.Player
{
    
    public class Portal : MonoBehaviour
    {
        [SerializeField] float distance;
    
        // Start is called before the first frame update
        void Start()
        {
    
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerMoveController playerController = collision.GetComponent<PlayerMoveController>();
                StartCoroutine(playerController.StageMove(distance));
            }
        }
    }
}
