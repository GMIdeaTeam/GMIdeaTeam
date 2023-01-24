using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class KillInfo
{
    public string monsterName;
    public int killCnt;
}

public class ClearController : MonoBehaviour
{
    string npcName = "";
    int wheelClickCnt = 0;
    bool haveKey = false;
    
    private List<KillInfo> killList = new List<KillInfo>();
    // 몬스터 처치 시 킬 리스트에 몬스터 이름, 킬 수 입력
    // 리스트에 몬스터 이름 없으면 몬스터와 숫자 1 삽입 (첫 킬일 때)
    // 몬스터 존재하면 killCnt++

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
