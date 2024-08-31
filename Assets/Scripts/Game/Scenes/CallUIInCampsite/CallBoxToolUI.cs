using ProjectBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBoxToolUI : PlayerInteraction
{
    public override void Interact()
    {
        UIManager.Instance.ShowPanel<BoxPanel>("BoxPanel", E_UI_Layer.top, (t) =>
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);
        });
        
    }
}
