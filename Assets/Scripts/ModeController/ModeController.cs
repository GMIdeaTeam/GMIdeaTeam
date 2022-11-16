using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    bool isEditMode = false;

    public Camera readCamera;
    public Camera editCamera;

    public bool checkEditMode()
    {
        return isEditMode;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // 다른 키로 바뀔 수도 있음
        {
            if (isEditMode)
                EditToRead();
            else
                ReadToEdit();
            
            isEditMode = !isEditMode;
        }
    }

    void ReadToEdit()
    {
        // 읽기 모드 -> 편집 모드
        ChangeToEditCamera();
    }

    void EditToRead()
    {
        // 편집 모드 -> 읽기 모드
        ChangeToReadCamera();
    }

    void ChangeToEditCamera()
    {
        readCamera.gameObject.SetActive(false);
        editCamera.gameObject.SetActive(true);
    }

    void ChangeToReadCamera()
    {
        readCamera.gameObject.SetActive(true);
        editCamera.gameObject.SetActive(false);
    }

    void ChangeSpeed()
    {
        // 이동 속도 변경
    }
}
