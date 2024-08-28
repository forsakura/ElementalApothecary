using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : PlayerInteraction
{
    public override void Interact()
    {
        Debug.Log("This is" + gameObject.name + "'s interact message.");
    }
}
