using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearInfo : MonoBehaviour
{
    public GameObject MonsterClear;
    public GameObject MissionClear;
    public GameObject GetItem;

    public GameObject InfoUI;

    private const int CNT = 500;
    private float UIWidth;

    private void Start()
    {
        UIWidth = InfoUI.GetComponent<RectTransform>().rect.width;
    }

    public void NoticeMonsterClear(string monsterName)
    {
        StartCoroutine(IShowInfoUI(MonsterClear, monsterName, "처치"));
    }
    
    public void NoticeMissionClear(string missionName)
    {
        StartCoroutine(IShowInfoUI(MissionClear, missionName, "완료"));
    }
    
    public void NoticeGetItem(string itemName)
    {
        StartCoroutine(IShowInfoUI(GetItem, itemName, "획득"));
    }

    IEnumerator IShowInfoUI(GameObject gameObj, string name, string action)
    {
        gameObj.SetActive(true);
        gameObj.GetComponent<Text>().text = name + " " + action;

        RectTransform UIRect = InfoUI.GetComponent<RectTransform>();
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
        gameObj.SetActive(false);
    }
}
