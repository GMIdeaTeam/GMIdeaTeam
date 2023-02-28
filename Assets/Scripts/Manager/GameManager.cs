using System;
using System.Collections;
using Idea.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Idea.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsEditMode { get; set; } = false;

        [SerializeField] Camera readCamera;
        [SerializeField] Camera editCamera;
        
        public static Action readToEditCallback;
        public static Action editToReadCallback;
        
        public override void Awake()
        {
            base.Awake();
            
            readToEditCallback += ChangeToEditCamera;
            editToReadCallback += ChangeToReadCamera;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) // 다른 키로 바뀔 수도 있음
            {
                if (IsEditMode)
                {
                    IsEditMode = false;
                    editToReadCallback.Invoke();
                }
                else
                {
                    IsEditMode = true;
                    readToEditCallback.Invoke();
                }
            }

            UnitTest();
        }

        /// <summary>
        /// 기능 테스트할 코드는 여기서 테스트 해봅시다.
        /// </summary>
        private void UnitTest()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ScreenHelper.FadeIn("Background", 5);
            }
        }
        
        private void ReadToEdit()
        {
            // 읽기 모드 -> 편집 모드
            ChangeToEditCamera();
        }

        private void EditToRead()
        {
            // 편집 모드 -> 읽기 모드
            ChangeToReadCamera();
        }
        
        private void ChangeToEditCamera()
        {
            readCamera.gameObject.SetActive(false);
            editCamera.gameObject.SetActive(true);
        }

        private void ChangeToReadCamera()
        {
            readCamera.gameObject.SetActive(true);
            editCamera.gameObject.SetActive(false);
        }
        
        public void OnPortal()
        {
            StartCoroutine(nameof(FadeInAndOut));
        }

        // 나중에 DOTween 사용
        private IEnumerator FadeInAndOut()
        {
            Debug.Log("Fade In");
            float t = 0;
            while (t <= 0.1f)
            {
                t += Time.deltaTime;
                Color color = Color.black;
                color.a = t / 0.1f;
                ResourceManager.Instance.fadePanel.color = color;
                yield return null;
            }
            while (t <= 0.4f)
            {
                t += Time.deltaTime;
                yield return null;
            }
            while (t <= 0.5f)
            {
                t += Time.deltaTime;
                Color color = Color.black;
                color.a = (0.5f - t) / 0.1f;
                ResourceManager.Instance.fadePanel.color = color;
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            Debug.Log("Fade Out");
            float t = 0.1f;
            while (t >= 0)
            {
                t += Time.deltaTime;
                ResourceManager.Instance.fadePanel.canvasRenderer.SetAlpha(255 * (t / 1));
                yield return null;
            }
        }
    }
}