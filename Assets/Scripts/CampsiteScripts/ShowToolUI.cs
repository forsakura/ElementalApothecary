using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ��Ҵ򿪹������
/// </summary>
public class ShowToolUI : PlayerInteraction
{
    public GameObject UIPannel;

    public override void Interact()
    {
        UIPannel.SetActive(!UIPannel.activeSelf);
    }
}
