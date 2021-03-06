﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBar : MonoBehaviour
{
    public GameObject play_button;
    public GameObject slide_button;
    public GameObject progress_bar;
    public GameObject debug_bar;

    public int step_no;

    public CodingInterfaceManager coding_manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToDebug()
    {
        slide_button.SetActive(true);
        progress_bar.SetActive(true);
        slide_button.GetComponent<StepSlider>().SetToStep0();
        debug_bar.GetComponent<DebugBar>().PopUp();
    }

    [ContextMenu("SwitchToCode")]
    public void SwitchToCode()
    {
        slide_button.SetActive(false);
        progress_bar.SetActive(false);
        play_button.GetComponent<PlayButton>().Reactivate();
        debug_bar.GetComponent<DebugBar>().Collapse();
    }

    public void UpdateStep(float slide_value)
    {
        int current_space_index = coding_manager.current_space_index;
        int step = (int)(Mathf.Floor(slide_value * coding_manager.debug_space[current_space_index].current_step)+1);
        if (step > coding_manager.debug_space[current_space_index].current_step) step = coding_manager.debug_space[current_space_index].current_step;
        step_no = step;     
        //foreach(int stp in coding_manager.debug_space.jump_index)
        for (int i = 0; i <= coding_manager.debug_space[current_space_index].jump_index.Count - 1; i++)
        {
            if (step_no >= coding_manager.debug_space[current_space_index].jump_index[i])
            {
                step += coding_manager.debug_space[current_space_index].jump_amount[i];
            }
        }       
        coding_manager.SetDebugStep(step);
        debug_bar.GetComponent<DebugBar>().SetDebugText();
    }

    public string GetDebugText()
    {
        return coding_manager.GetDebugText(step_no);
    }
}
