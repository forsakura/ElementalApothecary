using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumTooltipAttribute))]
public class ToolTipDreawer : PropertyAttribute
{
    private void OnGUI() 
    {
        
    }
}
public class EnumTooltipAttribute : PropertyAttribute
{
    public string tooltip;

    public EnumTooltipAttribute(string tooltip)
    {
        this.tooltip = tooltip;
    }
}