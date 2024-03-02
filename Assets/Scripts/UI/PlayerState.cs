using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayIamge;
    public Image PowerImage;
    public bool powerRecovering;
    public Character currentCharacter;

    private void Update()
    {
        if (healthDelayIamge.fillAmount>healthImage.fillAmount)
        {
            healthDelayIamge.fillAmount -= Time.deltaTime;
        }

        if (powerRecovering)
        {
            float percentage = currentCharacter.currentPower / currentCharacter.maxPower;
            PowerImage.fillAmount = percentage;
            if (percentage>=1)
            {
                powerRecovering = false;
                return;
            }
        }
    }

    /// <summary>
    /// 接受人物Healthy的值
    /// </summary>
    /// <param name="persentage">current/max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }

    public void OnPowerChange(Character character)
    {
        powerRecovering = true;
        currentCharacter = character;
    }
}
