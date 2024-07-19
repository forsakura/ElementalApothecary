using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 玩家打开工具面板
/// </summary>
public class ShowToolUI : PlayerInteraction
{
    public GameObject UIPannel;

    private void Update()
    {
        if (canOpenUI)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UIPannel.SetActive(!UIPannel.activeSelf);
            }
        }
    }
}
