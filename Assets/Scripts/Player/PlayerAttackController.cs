using System;
using System.Collections;
using UnityEngine;

namespace Idea.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        Animator playerAnimator;
        PlayerData playerData;
        
        static readonly int attack = Animator.StringToHash("attack");

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
            playerData = GetComponent<PlayerData>();
        }

        private void Update()
        {
            Attack();
        }
        
        /// <summary>
        /// 캐릭터 공격 함수
        /// </summary>
        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !playerData.isAttacking)
            {
                playerData.isAttacking = true;
                StartCoroutine(nameof(AttackDelay));
            }
        }
        
        private IEnumerator AttackDelay()
        {
            playerAnimator.SetTrigger(attack);
            // attackZone.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            // attackZone.SetActive(false);
            playerData.isAttacking = false;
        }
    }
}