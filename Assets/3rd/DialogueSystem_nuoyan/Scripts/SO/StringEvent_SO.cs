using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/StringEvent_SO")]
public class StringEvent_SO : ScriptableObject 
{
    public UnityAction<string> OneventRaised;

    public void RaiseEvent(string str)
    {
        OneventRaised?.Invoke(str);
    }
}
