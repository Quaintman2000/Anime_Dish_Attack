using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;

    public void AdjustHealthBarTo(float healthInPercent)
    {
        healthInPercent = Mathf.Clamp(healthInPercent, 0, 1);

        healthBarSlider.value = healthInPercent;
    }
}
