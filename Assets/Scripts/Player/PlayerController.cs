using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Idea.Player
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// 캐릭터가 움직이는 방향
        /// </summary>
        Vector2 moveVector;

        public ModeController.ModeController modeController;
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

        // Start is called before the first frame update
        void Start()
        {
            playerData = GetComponent<PlayerData>();
            playerAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            DamageInEditMode();
        }

        private void FixedUpdate()
        {
            PlayerMove();
            UpdateDirection();
        }

        private void LateUpdate()
        {
            UpdateAnimation();
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
            if (!isMovingStage)
            {
                moveVector.x = Input.GetAxisRaw("Horizontal");
                moveVector.y = Input.GetAxisRaw("Vertical");
            }

            transform.Translate(playerData.MoveSpeed * Time.deltaTime * moveVector);
        }


        public IEnumerator StageMove(PlayerData.Direction direction, float distance)
        {
            Debug.Log("Move!");

            isMovingStage = true;
            playerData.direction = direction;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = Vector3.zero;

            switch (direction)
            {
                case PlayerData.Direction.UP:
                    moveVector = Vector3.up;
                    endPosition = new Vector3(startPosition.x, startPosition.y + distance);
                    break;
                case PlayerData.Direction.RIGHT:
                    moveVector = Vector3.right;
                    endPosition = new Vector3(startPosition.x + distance, startPosition.y);
                    break;
                case PlayerData.Direction.DOWN:
                    moveVector = Vector3.down;
                    endPosition = new Vector3(startPosition.x, startPosition.y - distance);
                    break;
                case PlayerData.Direction.LEFT:
                    moveVector = Vector3.left;
                    endPosition = new Vector3(startPosition.x - distance, startPosition.y);
                    break;
            }

            yield return new WaitForSeconds(distance / playerData.MoveSpeed);

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

        /// <summary>
        /// 플레이어 애니메이션 클립 적용 함수
        /// </summary>
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

        /// <summary>
        /// 플레이어가 데미지를 입을 때 호출하게 되는 함수
        /// </summary>
        /// <param name="damage"></param>
        private void Damage(float damage)
        {
            playerData.HP -= damage;
        }

        /// <summary>
        /// 편집모드에서 플레이어가 지속적으로 받는 데미지
        /// </summary>
        private void DamageInEditMode()
        {
            if (modeController.IsEditMode) Damage(2.0f * Time.deltaTime);
        }

        IEnumerator DamageByMonster()
        {
            isBeingDamaged = true;
            while(collideMonsterNum > 0)
            {
                Damage(1.0f); // HP감소량은 몬스터의 공격력(추후 업데이트)
                yield return new WaitForSeconds(1.0f); // 대기시간은 몬스터의 공격대기시간(추후 업데이트)
            }
            isBeingDamaged = false;
        }
    }
}