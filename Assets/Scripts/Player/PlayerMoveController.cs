using System;
using System.Collections;
using Idea.Manager;
using Idea.Util;
using UnityEngine;

namespace Idea.Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        Rigidbody2D playerRigidbody;
        Animator playerAnimator;
        PlayerData playerData;
        
        Vector2 moveVector;
        static readonly int isMove = Animator.StringToHash("isMove");
        static readonly int direction = Animator.StringToHash("direction");

        bool CanMove => !playerData.isMovingStage;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            playerData = GetComponent<PlayerData>();
        }

        private void Update()
        {
            MovePlayer();
            UpdateDirection();
            UpdateMoveAnimation();
        }
        
        public void MovePlayer()
        {
            if (CanMove)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                var vertical = Input.GetAxisRaw("Vertical");

                var movement = playerData.MoveSpeed * Time.deltaTime * new Vector2(horizontal, vertical);
                playerRigidbody.MovePosition(playerRigidbody.position + movement);
                // transform.Translate(playerData.MoveSpeed * Time.deltaTime * moveVector);
            }
        }
        /// <summary>
        /// 플레이어 방향 정하는 함수
        /// </summary>
        private void UpdateDirection()
        {
            if (moveVector.x > 0 && moveVector.y == 0)
            {
                playerData.direction = Direction.RIGHT;
                // attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (moveVector.x < 0 && moveVector.y == 0)
            {
                playerData.direction = Direction.LEFT;
                // attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (moveVector.y > 0 && moveVector.x == 0)
            {
                playerData.direction = Direction.UP;
                // attackZone.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (moveVector.y < 0 && moveVector.x == 0)
            {
                playerData.direction = Direction.DOWN;
                // attackZone.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        private void UpdateMoveAnimation()
        {
            if (moveVector.x != 0 || moveVector.y != 0)
            {
                playerAnimator.SetBool(isMove, true);
            }
            else
            {
                playerAnimator.SetBool(isMove, false);
            }

            playerAnimator.SetFloat(direction, (float)playerData.direction);
        }
        /// <summary>
        /// 플레이어가 포탈을 탔을 때 실행되는 코루틴 함수
        /// </summary>
        /// <param name="direction">포탈의 플레이어 전송 방향</param>
        /// <param name="distance">포탈의 플레이어 전송 거리</param>
        /// <returns></returns>
        public IEnumerator StageMove(float distance)
        {
            if (playerData.isMovingStage) yield break;

            Debug.Log("Move!");

            playerData.isMovingStage = true;
            var direction = playerData.direction;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = Vector3.zero;

            switch (direction)
            {
                case Direction.UP:
                    moveVector = Vector3.up;
                    endPosition = new Vector3(startPosition.x, startPosition.y + distance);
                    break;
                case Direction.RIGHT:
                    moveVector = Vector3.right;
                    endPosition = new Vector3(startPosition.x + distance, startPosition.y);
                    break;
                case Direction.DOWN:
                    moveVector = Vector3.down;
                    endPosition = new Vector3(startPosition.x, startPosition.y - distance);
                    break;
                case Direction.LEFT:
                    moveVector = Vector3.left;
                    endPosition = new Vector3(startPosition.x - distance, startPosition.y);
                    break;
            }

            GameManager.Instance.OnPortal();
            yield return new WaitForSeconds(0.2f);

            transform.position = endPosition;
            playerData.isMovingStage = false;
        }
    }
}