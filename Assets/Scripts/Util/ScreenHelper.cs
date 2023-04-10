using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHelper : MonoBehaviour
{
    static readonly string path = "Prefab/Utility/ScreenHelper";
    static ScreenHelper instance = null;
    
    Dictionary<string, Image> imageDictionary = null;
    public static ScreenHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScreenHelper>();

                if (instance == null)
                {
                    GameObject screenHelper = Resources.Load<GameObject>(path);
                    screenHelper.name = nameof(ScreenHelper);
                    Instantiate(screenHelper);
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        imageDictionary = new Dictionary<string, Image>();
        var images = Instance.transform.GetComponentsInChildren<Image>(true);
        foreach (var image in images)
        {
            imageDictionary.Add(image.name, image);
        }
    }

    public static void FadeIn(string imageName, float duration)
    {
        Image target = null;
        if (Instance.imageDictionary.TryGetValue(imageName, out target))
        {
            if (!target.gameObject.activeSelf) target.gameObject.SetActive(true);
            target.DOFade(1, duration);
        }
    }

    public static void FadeOut(string imageName, float duration)
    {
        Image target = null;
        if (Instance.imageDictionary.TryGetValue(imageName, out target))
        {
            if (!target.gameObject.activeSelf) target.gameObject.SetActive(true);
            target.DOFade(0, duration);
        }
    }

    public static void FadeInOut(string imageName, float fadeInDuration, float fadeOutDuration)
    {
        Image target = null;
        if (Instance.imageDictionary.TryGetValue(imageName, out target))
        {
            if (!target.gameObject.activeSelf) target.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(target.DOFade(1, fadeInDuration));
            sequence.Append(target.DOFade(0, fadeOutDuration));
        }
    }
}