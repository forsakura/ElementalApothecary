using System;
using System.Collections.Generic;
using ProjectBase;
using UnityEngine;

namespace Managers
{
    /*
     * 统一管理UI面板，提供全局唯一UI面板的访问点
     */
    public class UIManager : MonoSingleton<UIManager>
    {
        public List<GameObject> panels;
        
        public GameObject loadingPanel;

        private void Start()
        {
            HideAllPanel();
        }

        public void HideAllPanel()
        {
            foreach (var panel in panels)
            {
                panel.SetActive(false);
            }
        }

        public void ShowPanel(GameObject panel)
        {
            foreach (var o in panels)
            {
                o.SetActive(o == panel);
            }
        }

        public void OnLoadGame()
        {
            
        }

        public void OnSetting()
        {
            
        }

        public void OnThanks()
        {
            
        }

        public void OnExitGame()
        {
            
        }
    }
}
