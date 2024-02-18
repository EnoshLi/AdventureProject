using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> EventRaised;

    public void EventRaise(Character character)
    {
        EventRaised?.Invoke(character);
    }
}
