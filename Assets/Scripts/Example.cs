using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public ModeController modeController;
    
    void Start()
    {
        if (modeController.checkEditMode() == true)
        {
            // 편집 모드 시 실행할 일
            gameObject.SetActive(true);
        }

        if (modeController.checkEditMode() == false)
        {
            // 읽기 모드 시 실행할 일
            gameObject.SetActive(false);
        }
    }
}
