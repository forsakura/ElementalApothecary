using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectBase.UI;

public class FunctionTablePanel : BasePanel
{
    public Button backBtn;
    public GameObject perFabFunction;
    public Transform content;
    public List<Fouction> fouctionList;
    protected override void Awake()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("FunctionTablePanel");
            UIManager.Instance.ShowPanel<PotPanel>("PotPanel", E_UI_Layer.top);
        });
        
    }
    public void AddFunction(int id1,int id2)
    {
        int id3 = InventoryManager.Instance.ReturnProductionID(id1, id2);
        if (id3 == 301) return;
        foreach (var fuction in fouctionList)
        {
            if (fuction.id1 == id1 && fuction.id2 == id2) return;
        }
        Fouction function = Instantiate(perFabFunction,content).GetComponent<Fouction>();
        function.id1 = id1;
        function.id2 = id2;
        function.id3 = id3;
        fouctionList.Add(function);
    }
}
