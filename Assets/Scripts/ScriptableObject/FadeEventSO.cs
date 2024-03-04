using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
   public UnityAction<Color, float, bool> onRisedEvent;
   /// <summary>
   /// 逐渐变黑
   /// </summary>
   /// <param name="duration"></param>
   public void FadeIn(float duration)
   {
      RiseEvent(Color.black, duration,true);
   }
   /// <summary>
   /// 逐渐变透明
   /// </summary>
   /// <param name="duration"></param>
   public void FadeOut(float duration)
   {
      RiseEvent(Color.clear, duration,false);
   }

   public void RiseEvent(Color target,float duration,bool fadeIn)
   {
      onRisedEvent?.Invoke(target,duration,fadeIn);
   }
}
