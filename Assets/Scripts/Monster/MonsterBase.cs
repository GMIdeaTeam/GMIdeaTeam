using System;
using Idea.Manager;
using UnityEngine;
using Idea.Util;
using Idea.Manager;
using Idea.Player;

namespace Idea.Monster
{
    public abstract class MonsterBase : MonoBehaviour
    {
        Animator monsterAnimator;
        SpriteRenderer monsterRenderer;
        Material originMaterial;
        Material distortionMaterial;
        
        [field:SerializeField] protected float MoveSpeed { get; set; }
        [field:SerializeField] protected int MaxHp { get; set; }
        [field:SerializeField] protected int CurrentHp { get; set; }

        protected bool IsDie => (CurrentHp <= 0);

        GameObject player;

        Vector2 moveVector;
        Direction direction = Direction.DOWN;

        private void Awake()
        {
            monsterRenderer = GetComponent<SpriteRenderer>();
            monsterAnimator = GetComponent<Animator>();
        }
        private void Start()
        {
            originMaterial = monsterRenderer.material;
            distortionMaterial = ResourceManager.Instance.distortionMaterial;

            monsterRenderer.material = GameManager.Instance.IsEditMode ? originMaterial : distortionMaterial;
            
            player = FindObjectOfType<PlayerData>().gameObject;
        }

        private void OnEnable()
        {
            GameManager.editToReadCallback += OnEditToRead;
            GameManager.readToEditCallback += OnReadToEdit;
        }

        private void OnDisable()
        {
            GameManager.editToReadCallback -= OnEditToRead;
            GameManager.readToEditCallback -= OnReadToEdit;
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

            UpdateAnimation();
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
            if (moveVector.x != 0 || moveVector.y != 0)
            {
                monsterAnimator.SetBool("isMove", true);
            }
            else
            {
                monsterAnimator.SetBool("isMove", true);
            }

            monsterAnimator.SetFloat("direction", (float)direction);
        }

        private void OnEditToRead()
        {
            monsterRenderer.material = distortionMaterial;
        }

        private void OnReadToEdit()
        {
            monsterRenderer.material = originMaterial;
        }

        /// <summary>
        /// 플레이어가 몬스터를 공격하는 데 성공하면 호출되는 함수
        /// </summary>
        /// <param name="damage"></param>
        public void OnDamage(int damage)
        {
            CurrentHp -= damage;
            if (IsDie) OnDie();
        }

        /// <summary>
        /// 몬스터가 죽었을 때 실행되는 함수
        /// 토끼 몬스터의 경우 이 함수를 상속받아서 override해주어야 함.
        /// </summary>
        protected virtual void OnDie()
        {
            // 죽었을 때 애니메이션??
            gameObject.SetActive(false);
        }
    }

}