using ProjectBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFlaskToolUI : PlayerInteraction
{
    public override void Interact()
    {
        UIManager.Instance.ShowPanel<FlaskPanel>("FlaskPanel", E_UI_Layer.top, (t) =>
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Flask, InventoryManager.Instance.flaskBag.itemList);
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);

        });
    }
}
