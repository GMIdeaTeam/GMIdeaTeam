using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

[System.Serializable]
public class MonsterClearInfo
{
    public string monsterName;
    public int killCnt;
}

public class ClearController : MonoBehaviour
{
    string npcName = "";
    int wheelClickCnt = 0;
    bool haveKey = false;

    private const int MONSTER_CNT = 2;
    
    public MonsterClearInfo[] MonsterClearList;
    // 처치해야 하는 몬스터 정보
    // 첫 몬스터 죽이면 이름 대조 후 죽여야 하는 수 받아옴
    // 몬스터 죽일 때 카운트 세서 다 죽이면 클리어
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetItemFromNPC();
        }
    }

    void GetItemFromNPC() // 특정 위치에서 npc에게 말 걸었을 때 획득 가능
    {
        print(npcName);
        if (npcName == "tinKnight")
        {
            // 도끼 획득
            print("도끼 획득");
            // 유령의 집 오픈
        }
        else if (npcName == "관람차") // 관람차 4번째칸
        {
            if (wheelClickCnt == 4)
            {
                //열쇠 획득
                haveKey = true;
                // 공터 오픈
            }
        }
    }

    public void OnClickWheelBtn()
    {
        if (wheelClickCnt < 4) 
            wheelClickCnt++;
        else if (wheelClickCnt == 4 && haveKey) // 5번째 클릭, 열쇠 획득한 상태
        {
            // 딸 튀어나옴
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        npcName = other.name;
    }
    
    
}
