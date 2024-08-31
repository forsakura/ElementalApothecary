
using TMPro;
using UnityEngine.UI;
using ProjectBase.UI;

public class PotPanel : BasePanel
{
    public Button sureBtn;
    public Button functionBtn;
    public InputField potionNum;
    public SlotUI input1;
    public SlotUI input2;
    public SlotUI output;
    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("SureBtn").onClick.AddListener(() =>
        {
            Refining();
            //FunctionTablePanel.Instance.AddFunction(input1.itemDetails.itemID, input2.itemDetails.itemID);
        });
        GetControl<Button>("FunctionBtn").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<FunctionTablePanel>("FunctionTablePanel", E_UI_Layer.top);
            UIManager.Instance.HidePanel("PotPanel");
        });
    }
    private void Update()
    {
        if (input1.itemAmount>0&&input2.itemAmount>0)
        {
            output.SetOutputMod();
        }
    }

    public void Refining()
    {
        int refineNum = int.Parse(potionNum.text);
        if (refineNum > 0)
        {
            if(input1.itemAmount>=refineNum&&input2.itemAmount>=refineNum)
            {
                var items = InventoryManager.Instance.potBag.itemList;
                int amount1 = items[0].itemAmount - refineNum;
                int amount2 = items[1].itemAmount - refineNum;
                InventoryManager.Instance.potBag.itemList[0]=new InventoryItem { itemID = items[0].itemID,itemAmount=amount1 };
                InventoryManager.Instance.potBag.itemList[1] = new InventoryItem { itemID = items[1].itemID, itemAmount = amount2 };

                InventoryManager.Instance.AddItem(output.itemDetails.ID, refineNum*10);
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Pot, InventoryManager.Instance.potBag.itemList);
            }
        }
    }
}
