using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class FunctionTablePanel : BasePanel<FunctionTablePanel>
{
    public Button backBtn;
    public GameObject perFabFunction;
    public Transform content;
    public List<Fouction> fouctionList;
    public override void Init()
    {
        backBtn.onClick.AddListener(() =>
        {
            HideMe();
            PotPanel.Instance.ShowMe();
        });
        HideMe();
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
