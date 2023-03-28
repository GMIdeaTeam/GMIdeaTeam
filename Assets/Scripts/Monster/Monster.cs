using System;
using System.Collections;
using System.Threading.Tasks;
using Idea.Manager;
using UnityEngine;
using Idea.Util;
using Idea.Manager;
using Random = UnityEngine.Random;

namespace Idea.Monster
{
    public class Monster : MonoBehaviour
    {
        [field: SerializeField] private float MoveSpeed { get; set; } = 1f;

        [SerializeField] int hp;

        private int HP
        {
            get => hp;
            set
            {
                if (hp <= 0) hp = 0;
                else hp = value;
            }
        }

        public bool IsDie => (HP <= 0);

        public GameObject player;

        Vector2 moveVector;
        Direction direction = Direction.DOWN;

        protected Animator monsterAnimator;
        Material originMaterial;

        protected Rigidbody2D rb;
        protected int hor, ver;

        private void Start()
        {
            originMaterial = GetComponent<SpriteRenderer>().material;
            GetComponent<SpriteRenderer>().material = ResourceManager.Instance.distortionMaterial;
            GameManager.editToReadCallback += OnEditToRead;
            GameManager.readToEditCallback += OnReadToEdit;
            //monsterAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            // 상속받은 몬스터가 자유이동일 때 FixedUpdate에 써줄 것
            rb.velocity = new Vector2(hor, ver);
        }

        protected void Wander()
        {
            hor = Random.Range(-1, 2);
            ver = Random.Range(-1, 2);

            if (hor > 0)
            {
                direction = Direction.RIGHT;
            }
            else if (hor < 0)
            {
                direction = Direction.LEFT;
            }
            else if (ver > 0)
            {
                direction = Direction.UP;
            }
            else if (ver < 0)
            {
                direction = Direction.DOWN;
            }
            
            UpdateAnimation();
            
            Invoke("Wander", Random.Range(1, 3));
        }

        /* 플레이어 주시형일 때 사용
         private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FollowPlayer();
                // 공격하기
            }
        }
        */
        
        protected void FollowPlayer()
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
            monsterAnimator.SetInteger("direction", (int)direction);
            /*if (moveVector.x != 0 || moveVector.y != 0)
            {
                monsterAnimator.SetBool("isMove", true);
            }
            else
            {*/
                monsterAnimator.SetBool("isMove", true);
            //}
        }

        private void OnEditToRead()
        {
            GetComponent<SpriteRenderer>().material = ResourceManager.Instance.distortionMaterial;
        }

        private void OnReadToEdit()
        {
            GetComponent<SpriteRenderer>().material = originMaterial;
        }

        /// <summary>
        /// 플레이어가 몬스터를 공격하는 데 성공하면 호출되는 함수
        /// </summary>
        /// <param name="damage"></param>
        public void OnDamage(int damage)
        {
            HP -= damage;
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