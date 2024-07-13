using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForeverEffectTable : BasePanel<ForeverEffectTable>
{
    public Button button;
    public override void Init()
    {
        button.onClick.AddListener(() =>
        {
            DistillerPanel.Instance.ShowMe();
            HideMe();
        });
        HideMe();
    }
}
