using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物理伤害由自己定义，元素伤害由Element类内部计算   currentDamage = physicDamage + attackElement.elementDamage;  
/// </summary>
public class Attack : MonoBehaviour
{
    [Tooltip("总伤害")]public float currentDamage;
    [HideInInspector]public Element attackElement;
    [Tooltip("物理伤害")]public float physicDamage;
    private void Awake() 
    {
        attackElement = GetComponent<Element>();
    }
    private void Update() 
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Element element = other.GetComponent<Element>();
        if (element != null) 
        {
            element.isAddOn = true;
            element.ElementReaction(attackElement,element,attackElement.currentElementCount,false);
            
            currentDamage = physicDamage + attackElement.elementDamage;  

            Debug.Log(attackElement.elementDamage);
            
            
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        other.GetComponent<Character>()?.GetHurt(this);
    }


}
