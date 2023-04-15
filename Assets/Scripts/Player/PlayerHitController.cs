using System.Collections;
using Idea.Manager;
using UnityEngine;

namespace Idea.Player
{
    public class PlayerHitController : MonoBehaviour
    {
        PlayerData playerData;

        private void Awake()
        {
            playerData = GetComponent<PlayerData>();
        }
        private void Update()
        {
            DamageInEditMode();
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
            if (GameManager.Instance.IsEditMode) Damage(2.0f * Time.deltaTime);
        }
    }
}