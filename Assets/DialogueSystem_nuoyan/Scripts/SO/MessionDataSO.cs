using System;
using UnityEngine;

[CreateAssetMenu( menuName = "任务/MessionDataSO"),Serializable]
public class MessionDataSO : ScriptableObject 
{
    public bool isTrack;
    public string messionName;
    [TextArea(4,5)]
    public string messionPosition;
    [TextArea(4,5)]
    public string messionDetails;

    private void OnDisable() 
    {
        isTrack = false;
    }
    
}
