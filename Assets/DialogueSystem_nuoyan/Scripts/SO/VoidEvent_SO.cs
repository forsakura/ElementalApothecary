using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/VoidEvent_SO")]
public class VoidEvent_SO : ScriptableObject 
{
    public UnityAction OneventRaised;

    public void RaiseEvent()
    {
        OneventRaised?.Invoke();
    }
}