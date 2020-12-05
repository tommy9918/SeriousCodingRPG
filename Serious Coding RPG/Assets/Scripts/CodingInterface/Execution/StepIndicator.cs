using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepIndicator : MonoBehaviour
{
    public Text stepText;
    // Start is called before the first frame update
    void Start()
    {
        SetStepText(0, 0);
        transform.localPosition = new Vector3(GetComponent<MoveTo>().startPosition.x, GetComponent<MoveTo>().startPosition.y, transform.localPosition.z);
    }    

    public void SetStepText(int current_step, int total_step)
    {
        stepText.text = "  STEP: \n" + current_step.ToString() + " / " + total_step.ToString();
    }

    public void Summon(int current_step, int total_step)
    {
        SetStepText(current_step, total_step);
        GetComponent<MoveTo>().ReplayMotion();
    }

    public void Hide()
    {
        GetComponent<MoveTo>().ReverseMotion();
    }
}
