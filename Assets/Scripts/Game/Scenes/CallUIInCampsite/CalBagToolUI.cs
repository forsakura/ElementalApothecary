using ProjectBase.UI;

public class CalBagToolUI : PlayerInteraction
{
    public override void Interact()
    {
        UIManager.Instance.ShowPanel<BagPanel>("BagPanel", E_UI_Layer.top, (t) =>
        {
            //UIManager.Instance.GetPanel("BagPanel").transform
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryManager.Instance.playerBag.itemList);
        });

    }
}
