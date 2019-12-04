using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Image bar;

    public void SetBar(float amount, float range)
    {
        bar.fillAmount = Mathf.Lerp(0, 1, (amount / range));
        CheckBounds();
    }

    private void CheckBounds()
    {
        if (bar.fillAmount > 1)
        {
            bar.fillAmount = 1f;
        }
        else if (bar.fillAmount < 0)
        {
            bar.fillAmount = 0f;
        }
    }

    public void EmptyBar()
    {
        bar.fillAmount = 0f;
    }

    public void FillBar()
    {
        bar.fillAmount = 1f;
    }
}
