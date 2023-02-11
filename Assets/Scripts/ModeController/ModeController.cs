using System;
using System.Collections;
using System.Collections.Generic;
using Idea.Player;
using Idea.Util;
using UnityEngine;

namespace Idea.Mode
{
    public class ModeController : Singleton<ModeController>
    {
        bool isEditMode = false;

        public Camera readCamera;
        public Camera editCamera;

        public PlayerData playerData;

        public bool IsEditMode
        {
            get { return isEditMode; }
            set { isEditMode = value; }
        }

        public static Action readToEditCallback;
        public static Action editToReadCallback;

        private void Awake()
        {
            readToEditCallback += ChangeToEditCamera;
            editToReadCallback += ChangeToReadCamera;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) // 다른 키로 바뀔 수도 있음
            {
                if (IsEditMode)
                    editToReadCallback.Invoke();
                else
                    readToEditCallback.Invoke();

                ChangeSpeed();
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
            if (!isEditMode) // 읽기 -> 편집
                playerData.MoveSpeed = 3f; // 추후 변경
            else // 편집 -> 읽기
                playerData.MoveSpeed = 5f;
        }
    }
}
