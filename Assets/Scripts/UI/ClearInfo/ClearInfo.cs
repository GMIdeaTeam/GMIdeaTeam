using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearInfo : MonoBehaviour
{
    public GameObject txt;

    public GameObject infoUI;

    private const int CNT = 500;
    private float UIWidth;

    private void Start()
    {
        UIWidth = infoUI.GetComponent<RectTransform>().rect.width;
    }

    public void NoticeMonsterClear(string monsterName)
    {
        StartCoroutine(IShowInfoUI(monsterName, "처치"));
    }
    
    public void NoticeMissionClear(string missionName)
    {
        StartCoroutine(IShowInfoUI(missionName, "완료"));
    }
    
    public void NoticeGetItem(string itemName)
    {
        StartCoroutine(IShowInfoUI(itemName, "획득"));
    }

    IEnumerator IShowInfoUI(string name, string action)
    {
        txt.SetActive(true);
        txt.GetComponent<Text>().text = name + " " + action;

        RectTransform UIRect = infoUI.GetComponent<RectTransform>();
        Vector3 defaultPos = UIRect.position;
        
        for (int i = 1; i <= CNT; i++)
        {
            UIRect.position += new Vector3(-UIWidth / CNT, 0, 0);
            yield return new WaitForSeconds(0.000001f);
        }
        
        yield return new WaitForSeconds(3.0f);
        
        for (int i = 1; i <= CNT; i++)
        {
            UIRect.position += new Vector3(UIWidth / CNT, 0, 0);
            yield return new WaitForSeconds(0.000001f);
        }

        UIRect.position = defaultPos;
        txt.SetActive(false);
    }
}
