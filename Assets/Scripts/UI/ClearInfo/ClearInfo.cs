using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearInfo : MonoBehaviour
{
    public GameObject MonsterClear;
    public GameObject MissionClear;
    public GameObject GetItem;

    public void NoticeMonsterClear(string monsterName, int kills, int total)
    {
        StartCoroutine(IWaitSeconds(MonsterClear, monsterName, kills, total, "처치"));
    }
    
    public void NoticeMissionClear(string missionName, int missionCnt, int total)
    {
        StartCoroutine(IWaitSeconds(MissionClear, missionName, missionCnt, total, "완료"));
    }
    
    public void NoticeGetItem(string itemName, int itemCnt, int total)
    {
        StartCoroutine(IWaitSeconds(GetItem, itemName, itemCnt, total, "획득"));
    }

    IEnumerator IWaitSeconds(GameObject gameObj, string name, int cnt, int total, string action)
    {
        gameObj.SetActive(true);
        gameObj.GetComponent<Text>().text = name + " ( " + cnt + " / " + total + " ) " + action;
        yield return new WaitForSeconds(2.0f);
        gameObj.SetActive(false);
    }
}
