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

        // 공격한 몬스터의 정보
        private struct MonsterInfo
        {
            public int attackPower; // 공격력
            public float attackWaitTime; // 공격 대기시간
        } MonsterInfo monsterInfo;
        int collideMonsterNum = 0;
        bool isBeingDamaged = false;

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                collideMonsterNum++;
                if (collideMonsterNum == 1 && !isBeingDamaged) // 처음 충돌한 몬스터
                {
                    StartCoroutine(nameof(Damage));
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

        IEnumerator Damage()
        {
            isBeingDamaged = true;
            while(collideMonsterNum > 0)
            {
                playerData.HP = playerData.HP - 1; // HP감소량은 몬스터의 공격력(추후 업데이트)
                yield return new WaitForSeconds(1.0f); // 대기시간은 몬스터의 공격대기시간(추후 업데이트)
            }
            isBeingDamaged = false;
        }
    }
}