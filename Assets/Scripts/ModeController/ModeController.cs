using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Idea.ModeController
{
    public class ModeController : MonoBehaviour
    {
        bool isEditMode = false;
    
        public Camera readCamera;
        public Camera editCamera;
    
        public bool IsEditMode
        {
            get
            {
                return isEditMode;
            }
            set
            {
                isEditMode = value;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) // 다른 키로 바뀔 수도 있음
            {
                if (IsEditMode)
                    EditToRead();
                else
                    ReadToEdit();
                
                IsEditMode = !IsEditMode;
            }
        }
    
        private void ReadToEdit()
        {
            // 읽기 모드 -> 편집 모드
            ChangeToEditCamera();
        }
    
        private void EditToRead()
        {
            // 편집 모드 -> 읽기 모드
            ChangeToReadCamera();
        }
    
        private void ChangeToEditCamera()
        {
            readCamera.gameObject.SetActive(false);
            editCamera.gameObject.SetActive(true);
        }
    
        private void ChangeToReadCamera()
        {
            readCamera.gameObject.SetActive(true);
            editCamera.gameObject.SetActive(false);
        }
    
        private void ChangeSpeed()
        {
            // 이동 속도 변경
        }
    }

}