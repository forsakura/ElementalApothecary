using ProjectBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fouction : MonoBehaviour
{
    public Image input1;
    public Image input2;
    public Image output;
    public Button button;

    public int id1;
    public int id2;
    public int id3;
    private void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("FunctionTablePanel");
            UIManager.Instance.ShowPanel<PotPanel>("PotPanel",E_UI_Layer.top); 
        });
    }

    public Fouction(int id1, int id2, int id3)
    {
        this.id1 = id1;
        this.id2 = id2;
        this.id3 = id3;
    }
    private void Update()
    {
        //input1.sprite = InventoryManager.Instance.GetItemDetails(id1).itemIcon;
        //input2.sprite = InventoryManager.Instance.GetItemDetails(id2).itemIcon;
        //output.sprite = InventoryManager.Instance.GetItemDetails(id3).itemIcon;
    }
}
