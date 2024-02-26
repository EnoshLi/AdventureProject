using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> EventRaised;

    public void EventRaise(Character character)
    {
        EventRaised?.Invoke(character);
    }
}
