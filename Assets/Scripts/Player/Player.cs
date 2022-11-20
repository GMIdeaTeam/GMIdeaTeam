using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Idea.Player
{
    public class Player : MonoBehaviour
    {
        public enum Direction
        {
            UP, RIGHT, DOWN, LEFT
        };
        [field: SerializeField]
        float MoveSpeed { get; set; } = 5f; // ModeController의 speed를 참조할 수 있도록 추후 변경

        Vector2 moveVector;
        Direction direction = Direction.DOWN;

        Animator playerAnimator;

        public ModeController modeController;

        // Start is called before the first frame update
        void Start()
        {
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

            transform.Translate(moveVector * Time.deltaTime * MoveSpeed);
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
                playerAnimator.SetBool("isMove", true);
            }
            else
            {
                playerAnimator.SetBool("isMove", false);
            }

            playerAnimator.SetFloat("direction", (float)direction);
        }
    }
}