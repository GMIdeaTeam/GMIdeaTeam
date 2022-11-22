using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Idea.Player
{
    public class PlayerController : MonoBehaviour
    {
        Vector2 moveVector;

        PlayerData playerData;
        Animator playerAnimator;

        // Start is called before the first frame update
        void Start()
        {
            playerData = GetComponent<PlayerData>();
            playerAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMove();
            UpdateDirection();
            UpdateAnimation();
        }

        private void PlayerMove()
        {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            moveVector.y = Input.GetAxisRaw("Vertical");

            transform.Translate(playerData.MoveSpeed * Time.deltaTime * moveVector);
        }

        private void UpdateDirection()
        {
            if (moveVector.x > 0 && moveVector.y == 0)
            {
                playerData.direction = PlayerData.Direction.RIGHT;
            }
            else if (moveVector.x < 0 && moveVector.y == 0)
            {
                playerData.direction = PlayerData.Direction.LEFT;
            }
            else if (moveVector.y > 0 && moveVector.x == 0)
            {
                playerData.direction = PlayerData.Direction.UP;
            }
            else if (moveVector.y < 0 && moveVector.x == 0)
            {
                playerData.direction = PlayerData.Direction.DOWN;
            }
        }

        private void UpdateAnimation()
        {
            if (moveVector.x != 0 || moveVector.y != 0)
            {
                playerAnimator.SetBool("isMove", true);
            }
            else
            {
                playerAnimator.SetBool("isMove", false);
            }

            playerAnimator.SetFloat("direction", (float)playerData.direction);
        }
    }
}