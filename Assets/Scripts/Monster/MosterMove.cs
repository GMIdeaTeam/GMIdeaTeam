using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MosterMove : MonoBehaviour
{
    public enum Direction
    {
        UP, RIGHT, DOWN, LEFT
    };
    
    [field: SerializeField]
    float MoveSpeed { get; set; } = 1f;

    Vector2 moveVector;
    Direction direction = Direction.DOWN;

    Animator monsterAnimator;

    GameObject player;

    private void Start()
    {
        monsterAnimator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FollowPlayer();
            // 공격하기
        }
    }

    private void FollowPlayer()
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            direction = Direction.RIGHT;
            // 오른쪽
        }
        else if (player.transform.position.x < gameObject.transform.position.x)
        {
            direction = Direction.LEFT;
            // 왼쪽
        }
        else if (player.transform.position.y > gameObject.transform.position.y)
        {
            direction = Direction.UP;
            // 위쪽
        }
        else if (player.transform.position.y < gameObject.transform.position.y)
        {
            direction = Direction.DOWN;
            // 아래
        }

        // moveVector = player.transform.position - gameObject.transform.position; // 상대적인 위치

        transform.position =
            Vector2.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void UpdateDirection()
    {
        if (moveVector.x > 0 && moveVector.y == 0)
        {
            direction = Direction.RIGHT;
        }
        else if (moveVector.x < 0 && moveVector.y == 0)
        {
            direction = Direction.LEFT;
        }
        else if (moveVector.y > 0 && moveVector.x == 0)
        {
            direction = Direction.UP;
        }
        else if (moveVector.y < 0 && moveVector.x == 0)
        {
            direction = Direction.DOWN;
        }
    }

    private void UpdateAnimation()
    {
        //if (moveVector.x != 0)
        //{
        //    playerAnimator.SetBool("isMoveX", true);
        //}
        //else
        //{
        //    playerAnimator.SetBool("isMoveX", false);
        //}
    
        //if (moveVector.y != 0)
        //{
        //    playerAnimator.SetBool("isMoveY", true);
        //}
        //else
        //{
        //    playerAnimator.SetBool("isMoveY", false);
        //}
    
        //playerAnimator.SetFloat("inputX", moveVector.x);
        //playerAnimator.SetFloat("inputY", moveVector.y);
    
        if (moveVector.x != 0 || moveVector.y != 0)
        {
            monsterAnimator.SetBool("isMove", true);
        }
        else
        {
            monsterAnimator.SetBool("isMove", false);
        }
    
        monsterAnimator.SetFloat("direction", (float)direction);
    }
}
