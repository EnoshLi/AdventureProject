using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRised;

    public void EventRise()
    {
        OnEventRised?.Invoke();
    }
}
