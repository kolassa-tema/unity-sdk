﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Aryzon
{
    public class Aryzon2DUI : MonoBehaviour, IAryzonEventHandler
    {
        public GameObject startUI;
        public GameObject phoneInHeadsetUI;
        public GameObject settingsUI;

        private GameObject closeButton;
        private GameObject CloseButton
        {
            get
            {
                if (!closeButton)
                {
                    closeButton = phoneInHeadsetUI.transform.Find("Back").gameObject;
                }
                return closeButton;
            }
        }

        private void Start()
        {
            AryzonSettings.Instance.RegisterEventHandler(this);

            if (AryzonSettings.Instance.aryzonManager.setAryzonModeOnStart)
            {
                CloseButton.SetActive(false);
            }
            else
            {
                CloseButton.SetActive(true);
            }

            if (AryzonSettings.Instance.aryzonManager.aryzonMode && !AryzonSettings.Instance.aryzonManager.stereoscopicMode)
            {
                ShowMainUI();
            }
        }

        private void OnDestroy()
        {
            if (AryzonSettings.Instance)
            {
                AryzonSettings.Instance.UnregisterEventHandler(this);
            }
        }

        public void ShowSettingsUI()
        {
            startUI.SetActive(false);
            phoneInHeadsetUI.SetActive(false);
            settingsUI.SetActive(true);
        }

        public void ShowMainUI()
        {
            startUI.SetActive(false);
            phoneInHeadsetUI.SetActive(true);
            settingsUI.SetActive(false);
        }

        public void StartAryzonMode()
        {
            AryzonSettings.Instance.aryzonManager.StartAryzonMode();

            if (AryzonSettings.Instance.aryzonManager.setAryzonModeOnStart)
            {
                CloseButton.SetActive(false);
            } else
            {
                CloseButton.SetActive(true);
            }
        }
        public void StopAryzonMode()
        {
            AryzonSettings.Instance.aryzonManager.StopAryzonMode();
            Debug.Log("Stop!");
        }

        public void OnStopStereoscopicMode(AryzonModeEventArgs e)
        {
            phoneInHeadsetUI.SetActive(true);
        }

        public void OnStartStereoscopicMode(AryzonModeEventArgs e)
        {
            phoneInHeadsetUI.SetActive(false);
        }

        public void OnStopAryzonMode(AryzonModeEventArgs e)
        {
            startUI.SetActive(true);
            phoneInHeadsetUI.SetActive(false);
        }

        void IAryzonEventHandler.OnStartAryzonMode(AryzonModeEventArgs e)
        {
            startUI.SetActive(false);
            phoneInHeadsetUI.SetActive(true);
        }
    }
}