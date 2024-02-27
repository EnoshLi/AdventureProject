using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/PlayerAudioEventSO")]
public class PlayerAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> onEventRised;

    public void EventRise(AudioClip audioClip)
    {
        onEventRised?.Invoke(audioClip);
    }

}
