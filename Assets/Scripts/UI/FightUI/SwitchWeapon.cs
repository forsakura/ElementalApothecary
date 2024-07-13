using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchWeapon : Singleton<SwitchWeapon>
{
    public Image throwPotion;
    public Image shootPotion;

    EPlayerAttackState currentMode;
    Vector2 frontMin = new Vector2(0.0f, 0.1f);
    Vector2 frontMax = new Vector2(0.9f, 1.0f);
    Vector2 behindMin = new Vector2(0.1f, 0.0f);
    Vector2 behindMax = new Vector2(1.0f, 0.9f);

    void Start()
    {
        // 初始默认模式是啥我不知道
        currentMode = EPlayerAttackState.Throwing;
        SwitchWeaponTo(EPlayerAttackState.Throwing);
    }

    public void SwitchWeaponTo(EPlayerAttackState state)
    {
        currentMode = state;
        switch(currentMode)
        {
            case EPlayerAttackState.Throwing:
                throwPotion.GetComponent<RectTransform>().anchorMin = frontMin;
                throwPotion.GetComponent<RectTransform>().anchorMax = frontMax;
                shootPotion.GetComponent<RectTransform>().anchorMin = behindMin;
                shootPotion.GetComponent<RectTransform>().anchorMax = behindMax;
                throwPotion.transform.SetAsLastSibling();
                break;
            case EPlayerAttackState.Shooting:
                throwPotion.GetComponent<RectTransform>().anchorMin = behindMin;
                throwPotion.GetComponent<RectTransform>().anchorMax = behindMax;
                shootPotion.GetComponent<RectTransform>().anchorMin = frontMin;
                shootPotion.GetComponent<RectTransform>().anchorMax = frontMax;
                shootPotion.transform.SetAsLastSibling();
                break;
        }
    }

    public void SwitchWeaponToAnother()
    {
        if (currentMode == EPlayerAttackState.Throwing)
        {
            SwitchWeaponTo(EPlayerAttackState.Shooting);
        }
        else if(currentMode == EPlayerAttackState.Shooting)
        {
            SwitchWeaponTo(EPlayerAttackState.Throwing);
        }
    }

    public void Test()
    {
        SwitchWeaponToAnother();
    }
}