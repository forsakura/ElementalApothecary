using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ͼƬ��Ԫ�����ڿ�ĳ�����Ϊ8:1�����޸�Canvas������ͬ���޸�Ԫ�����߶�
public class BarController : MonoBehaviour
{
    public CharacterRestriction characterRestriction;
    private Color Aqua = new Color(0.0f, 0.0f, 1.0f);
    private Color Terra = new Color(1.0f, (240.0f / 255.0f), 0.0f);
    private Color Aer = new Color(0.0f, 1.0f, 0.0f);
    private Color Ignis = new Color(1.0f, 0.0f, 0.0f);

    [SerializeField]
    Image AerTerra;
    [SerializeField]
    Image IgnisAqua;
    [SerializeField]
    Image LeftDelayBar;
    [SerializeField]
    Image RightDelayBar;
    [SerializeField]
    Image HPBar;


    private void OnEnable() 
    {
        characterRestriction.OnEventRaised += OnCharacterInformationChange;
    }
    private void OnDisable() 
    {
        characterRestriction.OnEventRaised -= OnCharacterInformationChange;
    }

    private void OnCharacterInformationChange(Character character)//同步的数据都写在这个事件里面了
    {
        HPBar.fillAmount = character.currentHp / characterRestriction.CharacterRestrictionEntity[character.currentLv].maxHp;

        AerTerra.fillAmount = Mathf.Abs(character.element.currentElementCount.x / characterRestriction.CharacterRestrictionEntity[character.currentLv].maxAerTerra);
        
        IgnisAqua.fillAmount = Mathf.Abs(character.element.currentElementCount.y / characterRestriction.CharacterRestrictionEntity[character.currentLv].maxIgnisAqua);

        AerTerra.color = character.element.currentElementCount.x > 0 ? Aer : Terra;
        IgnisAqua.color = character.element.currentElementCount.y > 0 ? Ignis : Aqua;
    }

    public void ElementBarUpdate(Character character)
    {
        
    }

    // public void HPBarUpdate()
    // {
    //     HPBar.fillAmount = character.currentHp / character.characterRestriction_SO.maxHp;

    //     character.OnCharacterInformationChange?.Invoke(character);
    // }
}
