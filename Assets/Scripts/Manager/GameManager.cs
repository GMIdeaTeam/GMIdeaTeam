using System.Collections;
using Idea.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Idea.Manager
{
    public class GameManager : Singleton<GameManager>
    {
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