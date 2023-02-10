using System;
using UnityEngine;

namespace Idea.Player
{
    public class AttackInfo : MonoBehaviour
    {
        [SerializeField] int attackPower = 5;

        private void OnEnable()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Monster"))
            {
                collider.GetComponent<Monster.Monster>().OnDamage(attackPower);
            }
        }
    }
}