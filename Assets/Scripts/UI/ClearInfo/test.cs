using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    ClearInfo clear;

    void Start()
    {
        clear = GetComponent<ClearInfo>();
        clear.NoticeGetItem("열쇠");
        // clear.NoticeMissionClear("킬", 1, 1);
        //clear.NoticeMonsterClear("거미");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
