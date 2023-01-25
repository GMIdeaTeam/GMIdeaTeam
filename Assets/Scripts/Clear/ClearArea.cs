using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour
{
    public GameObject targetObj;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == targetObj.name && GetComponent<Collider2D>().bounds.Contains(other.bounds.max) &&
            GetComponent<Collider2D>().bounds.Contains(other.bounds.min))
        {
            if (other.name == "BlueButterfly")
            {
                // 유령의 집 오픈
            }
        }
    }
}
