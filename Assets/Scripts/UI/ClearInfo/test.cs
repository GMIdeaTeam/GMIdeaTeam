using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    ClearInfo clear;

    void Start()
    {
        clear = GetComponent<ClearInfo>();
        // clear.NoticeGetItem("밴달", 1, 1);
        // clear.NoticeMissionClear("킬", 1, 1);
        clear.NoticeMonsterClear("적팀", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
