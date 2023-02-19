using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Idea.Util;
using Idea.Manager;
using Idea.Mode;

namespace Idea.Player
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// 캐릭터가 움직이는 방향
        /// </summary>
        Vector2 moveVector;

        public ModeController modeController;
        Rigidbody2D playerRigidbody;
        PlayerData playerData;
        Animator playerAnimator;

        // 공격한 몬스터의 정보
        private struct MonsterInfo
        {
            public int attackPower; // 공격력
            public float attackWaitTime; // 공격 대기시간
        } MonsterInfo monsterInfo;
        int collideMonsterNum = 0;
        bool isBeingDamaged = false;

        bool isMovingStage = false;
        bool isAttacking = false;
        private bool CanMove
        {
            get
            {
                return !isMovingStage;
            }
        }

        [SerializeField] GameObject attackZone;

        // Start is called before the first frame update
        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerData = GetComponent<PlayerData>();
            playerAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDirection();
            DamageInEditMode();
            Attack();
        }

        void FixedUpdate()
        {
            PlayerMove();
        }

        private void LateUpdate()
        {
            UpdateMoveAnimation();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                collideMonsterNum++;
                if (collideMonsterNum == 1 && !isBeingDamaged) // 처음 충돌한 몬스터
                {
                    StartCoroutine(DamageByMonster());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                collideMonsterNum--;
            }
        }

        /// <summary>
        /// 플레이어 이동 함수
        /// </summary>
        private void PlayerMove()
        {
            if (CanMove)
            {
                // if (Input.GetButtonDown("Horizontal"))
                // {
                //     playerRigidbody.AddForce(new Vector2(playerData.MoveSpeed, 0));
                // }
                // if (Input.GetButtonDown("Vertical"))
                // {
                //     playerRigidbody.AddForce(new Vector2(0, playerData.MoveSpeed));
                // }
                // if (Input.GetButtonUp("Horizontal"))
                // {
                //     playerRigidbody.AddForce(new Vector2(-playerData.MoveSpeed, 0));
                // }
                // if (Input.GetButtonUp("Vertical"))
                // {
                //     playerRigidbody.AddForce(new Vector2(0, -playerData.MoveSpeed));
                // }
                var horizontal = Input.GetAxisRaw("Horizontal");
                var vertical = Input.GetAxisRaw("Vertical");

                var movement = playerData.MoveSpeed * Time.deltaTime * new Vector2(horizontal, vertical);
                playerRigidbody.MovePosition(playerRigidbody.position + movement);
                // transform.Translate(playerData.MoveSpeed * Time.deltaTime * moveVector);
            }

        }

        /// <summary>
        /// 플레이어가 포탈을 탔을 때 실행되는 코루틴 함수
        /// </summary>
        /// <param name="direction">포탈의 플레이어 전송 방향</param>
        /// <param name="distance">포탈의 플레이어 전송 거리</param>
        /// <returns></returns>
        public IEnumerator StageMove(float distance)
        {
            if (isMovingStage) yield break;

            Debug.Log("Move!");

            isMovingStage = true;
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
            isMovingStage = false;
        }

        /// <summary>
        /// 플레이어 방향 정하는 함수
        /// </summary>
        private void UpdateDirection()
        {
            if (moveVector.x > 0 && moveVector.y == 0)
            {
                playerData.direction = Direction.RIGHT;
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (moveVector.x < 0 && moveVector.y == 0)
            {
                playerData.direction = Direction.LEFT;
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (moveVector.y > 0 && moveVector.x == 0)
            {
                playerData.direction = Direction.UP;
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (moveVector.y < 0 && moveVector.x == 0)
            {
                playerData.direction = Direction.DOWN;
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        /// <summary>
        /// 플레이어 애니메이션 클립 적용 함수
        /// </summary>
        private void UpdateMoveAnimation()
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

        /// <summary>
        /// 플레이어가 데미지를 입을 때 호출하게 되는 함수
        /// </summary>
        /// <param name="damage"></param>
        private void Damage(float damage)
        {
            playerData.HealthPoint -= damage;
        }

        /// <summary>
        /// 편집모드에서 플레이어가 지속적으로 받는 데미지
        /// </summary>
        private void DamageInEditMode()
        {
            if (modeController.IsEditMode) Damage(2.0f * Time.deltaTime);
        }

        private IEnumerator DamageByMonster()
        {
            isBeingDamaged = true;
            while(collideMonsterNum > 0)
            {
                Damage(1.0f); // HP감소량은 몬스터의 공격력(추후 업데이트)
                yield return new WaitForSeconds(1.0f); // 대기시간은 몬스터의 공격대기시간(추후 업데이트)
            }
            isBeingDamaged = false;
        }

        /// <summary>
        /// 캐릭터 공격 함수
        /// </summary>
        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                isAttacking = true;
                StartCoroutine(nameof(AttackDelay));
            }
        }
        
        private IEnumerator AttackDelay()
        {
            playerAnimator.SetTrigger("attack");
            attackZone.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            attackZone.SetActive(false);
            isAttacking = false;
        }
    }
}