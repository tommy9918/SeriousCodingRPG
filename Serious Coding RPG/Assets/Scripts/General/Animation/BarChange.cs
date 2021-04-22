using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChange : MonoBehaviour
{
    float duration = 15f;
    public float fill_amount = 1f;
    public float change_amount;
    public float target_amount;

    public bool increasing;
    public bool decreasing;
    

    // Update is called once per frame
    void Update()
    {
        if (decreasing)
        {
            if (fill_amount > target_amount)
            {
                fill_amount -= change_amount;
                if (fill_amount < target_amount)
                {
                    fill_amount = target_amount;
                    decreasing = false;
                }
                GetComponent<Image>().fillAmount = fill_amount;
            }
        }
        else if (increasing)
        {
            if (fill_amount < target_amount)
            {
                fill_amount += change_amount;
                if (fill_amount > target_amount)
                {
                    fill_amount = target_amount;
                    increasing = false;
                }
                GetComponent<Image>().fillAmount = fill_amount;
            }
        }
    }

    public void ChangeTo(float fill)
    {
        target_amount = fill;
        if(target_amount > GetComponent<Image>().fillAmount)
        {
            increasing = true;
            decreasing = false;
            change_amount = (target_amount - GetComponent<Image>().fillAmount) / duration;
        }
        else if(GetComponent<Image>().fillAmount > target_amount)
        {
            decreasing = true;
            increasing = false;
            change_amount = (GetComponent<Image>().fillAmount - target_amount) / duration;
        }
    }
}
