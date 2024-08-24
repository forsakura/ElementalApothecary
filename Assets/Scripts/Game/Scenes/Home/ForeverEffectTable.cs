using ProjectBase.UI;
using UnityEngine;
using UnityEngine.UI;

public class ForeverEffectTable : BasePanel
{
    
    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("PotBtn").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<DistillerPanel>("DistillerPanel", E_UI_Layer.top);
            UIManager.Instance.HidePanel("ForeverEffectTable");
        });
    }
}
