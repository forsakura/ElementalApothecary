using ProjectBase.UI;
using System;
using System.Reflection;
using UnityEngine;

public class InteractTest : PlayerInteraction
{
    
    public override void Interact()
    {
        Debug.Log("This is" + gameObject.name + "'s interact message.");
    }
}
