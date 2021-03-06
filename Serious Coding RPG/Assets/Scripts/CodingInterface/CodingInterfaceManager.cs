﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CodingInterfaceManager : MonoBehaviour
{
    public static CodingInterfaceManager Instance;

    public string questID;
    public Sprite[] runes;
    public GameObject rune_ref;
    public List<GameObject> runes_summoned;
    public GameObject magic_circle;
    public GameObject coding_area;
    public PlayBar playbar;
    [Space(10)]
    public GameObject block_selection;
    public GameObject block_selection_scroll_list;
    public GameObject coding_scroll_list;
    public GameObject DarkLayerInstance;
    public GameObject DarkLayer;
    public GameObject detail_requirement;
    public MainCodeArea main_code_area;
    [Space(10)]
    public List<CommandBlock> coding_blocks;
    public List<string> quest_inputs;
    public List<string> expect_outputs;
    [Space(10)]
    public GameObject step_indicator;
    public DebugBar debug_bar;

    [Space(10)]
    public GameObject active_dragging_block;
    [Space(10)]
    public List<ExecutionSpace> debug_space;
    public List<string> debug_expected_output;
    public List<string> debug_real_output;
    public List<bool> passed;
    public int current_space_index;
    [Space(10)]
    public CodeRequirment code_req;
    public GameObject finish_button;
    public GameObject add_icon;
    public Sprite add;
    public Sprite edit;
    public bool all_passed;

    public GameObject curse;
    public bool cursed;
    public bool toolong;
    public bool zerodivision;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCodingUI();
        
    }

    public void InitializeCodingUI()
    {
        coding_blocks = new List<CommandBlock>();
        quest_inputs = new List<string>();
        expect_outputs = new List<string>();
        if (questID != null && questID != "")
        {
            Quest current_quest = QuestManager.Instance.getQuestFromID(questID);
            for (int i = 0; i <= current_quest.input.Length - 1; i++)
            {
                quest_inputs.Add(current_quest.input[i]);
                expect_outputs.Add(current_quest.output[i]);
            }
        }
        code_req.SetRequirementText(questID);
        debug_space = new List<ExecutionSpace>();

        switch (questID)
        {
            case "MAIN1":     //hello mana mission
                Player.Instance.UnlockHelloWorldBlock();
                break;
            case "MAIN2":     //sum mission
                Player.Instance.UnlockCalculateBlock();
                break;
            case "MAIN3":     //max mission
                Player.Instance.UnlockIfRelatedBlock();
                break;
            case "MAIN5":     //loop apple mission
                Player.Instance.UnlockJumpBlock();
                break;
            case "MAIN???":     //array-related mission 1, tbc
                Player.Instance.UnlockAtBlock();
                break;
        }

        //Debug.Log(coding_blocks);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("GetAllCommand")]
    public void GetAllCommand()
    {
        coding_blocks.Clear();
        List<GameObject> coding_block_obj_array = main_code_area.coding_blocks;
        for (int i = 0; i <= coding_block_obj_array.Count - 1; i++)
        {
            coding_blocks.Add(new CommandBlock(coding_block_obj_array[i].GetComponent<BlockManager>()));
        }
        //coding_blocks = Player.Instance.data.skills[0].GetOriginalCommandBlockList();
    }

    [ContextMenu("SaveSkill")]
    public void SaveSkill()
    {
        GetAllCommand();
        string skill_name = "power";
        int input_slots = 2;
        Player.Instance.data.skills.Add(new Skill(coding_blocks, skill_name, input_slots));
    }

    public void SwitchDebugSpace(int index)
    {
        coding_area.SetActive(true);
        current_space_index = index;
        playbar.SwitchToDebug();
        if (passed[index])
        {
            debug_bar.SetRight();
        }
        else
        {
            debug_bar.SetWrong();
        }
        step_indicator.GetComponent<StepIndicator>().Summon(1, debug_space[current_space_index].TotalStep());
        Debug.Log(index);
    }

    void OnTouchUp()
    {
        if (debug_space.Count > 0)
        {
            coding_area.SetActive(false);
            //playbar.SwitchToDebug();
            debug_bar.Collapse();
            step_indicator.GetComponent<StepIndicator>().Hide();
        }
    }

    public void EditCode()
    {
        add_icon.GetComponent<SpriteRenderer>().sprite = add;
        add_icon.GetComponent<ChildButton>().ButtonUse = "OpenBlockSelection";
        step_indicator.GetComponent<StepIndicator>().Hide();
        coding_area.SetActive(true);
        finish_button.SetActive(false);
        foreach(GameObject rune in runes_summoned)
        {
            Destroy(rune);
        }
        runes_summoned.Clear();
        debug_space.Clear();
        passed.Clear();
        debug_real_output.Clear();
        debug_expected_output.Clear();
        cursed = false;
        toolong = false;
        zerodivision = false;
        playbar.SwitchToCode();
    }

    [ContextMenu("RunCode")]
    public void RunCode()
    {
        add_icon.GetComponent<SpriteRenderer>().sprite = edit;
        add_icon.GetComponent<ChildButton>().ButtonUse = "EditCode";
        all_passed = true;
        GetAllCommand();
        coding_area.SetActive(false);
        //Player.Instance.data.skills.Add(new Skill(coding_blocks, "Sum", 2));
        for (int i = 0; i <= quest_inputs.Count - 1; i++)
        {
            ExecutionSpace exec = new ExecutionSpace();
            string output = exec.StartExecution(coding_blocks, quest_inputs[i]);
            if (output != null && output.Length > 0)
            {
                Debug.Log("The program output: " + output+"    Expected output: "+expect_outputs[i]);               
            }
            else
            {
                Debug.Log("The program output nothing.");
                
            }
            if (output.Equals(expect_outputs[i]))
            {
                Debug.Log("Test case passed!");
                debug_space.Add(exec);
                debug_expected_output.Add(expect_outputs[i]);
                debug_real_output.Add(output);
                passed.Add(true);
                //step_indicator.GetComponent<StepIndicator>().Summon(1, debug_space.TotalStep());
            }
            else
            {
                all_passed = false;
                //Debug.Log("Expected output is: " + expect_outputs[i]);
                Debug.Log("Test case failed! Try again?");
                debug_space.Add(exec);
                debug_expected_output.Add(expect_outputs[i]);
                debug_real_output.Add(output);
                passed.Add(false);
                //step_indicator.GetComponent<StepIndicator>().Summon(1, debug_space.TotalStep());
                //debug_bar.SetWrong();
                //break;
            }

            //float radius = 3.68f;
            //Vector3 centre = magic_circle.transform.position;

            //float x = (float)(centre.x + radius * Math.Sin((double)((360f / expect_outputs.Count * i) * Mathf.Deg2Rad)));
            //float y = (float)(centre.y + radius * Math.Cos((double)((360f / expect_outputs.Count * i) * Mathf.Deg2Rad)));
            //Vector2 pos = new Vector2(x, y);
            //runes_summoned.Add(Instantiate(rune_ref, pos, Quaternion.identity, transform));
            //runes_summoned[i].GetComponent<Rune>().rune_sprite.sprite = runes[i];
            //runes_summoned[i].transform.localPosition = new Vector3(runes_summoned[i].transform.localPosition.x, runes_summoned[i].transform.localPosition.y, - 0.1f);                     

            //if (passed[i])
            //{
            //    runes_summoned[i].GetComponent<Rune>().SetRight(i);
            //    runes_summoned[i].GetComponent<FadeControl>().StartFadeIn();
            //}
            //else
            //{
            //    runes_summoned[i].GetComponent<Rune>().SetWrong(i);
            //    runes_summoned[i].GetComponent<FadeControl>().StartFadeIn();
            //}
        }
        if (cursed)
        {
            GameObject temp = Instantiate(curse, transform);
            Destroy(temp, 2f);
            if (zerodivision)
            {
                GameManager.Instance.Notification("<b><i><color=#FF0000>You Have Been Cursed by Zero Division</color>\nCredit Point Decreased</i></b>");          
            }
            else if (toolong)
            {
                GameManager.Instance.Notification("<b><i><color=#FF0000>You Have Been Cursed by Spell Overflow</color>\nCredit Point Decreased</i></b>");
            }
        }
        StartCoroutine(SummonRunes());

        if (all_passed)
        {
            finish_button.SetActive(true);
        }
    }

    public IEnumerator SummonRunes()
    {
        for(int i = 0;i <= quest_inputs.Count - 1; i++)
        {
            float radius = 3.68f;
            Vector3 centre = magic_circle.transform.position;

            float x = (float)(centre.x + radius * Math.Sin((double)((360f / expect_outputs.Count * i) * Mathf.Deg2Rad)));
            float y = (float)(centre.y + radius * Math.Cos((double)((360f / expect_outputs.Count * i) * Mathf.Deg2Rad)));
            Vector2 pos = new Vector2(x, y);
            runes_summoned.Add(Instantiate(rune_ref, pos, Quaternion.identity, transform));
            runes_summoned[i].GetComponent<Rune>().rune_sprite.sprite = runes[i];
            runes_summoned[i].transform.localPosition = new Vector3(runes_summoned[i].transform.localPosition.x, runes_summoned[i].transform.localPosition.y, -0.1f);

            if (passed[i])
            {
                //runes_summoned[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                //runes_summoned[i].GetComponentInChildren<SpriteRenderer>().color = new Color(0, 1, 0);
                runes_summoned[i].GetComponent<Rune>().SetRight(i);
                //runes_summoned[i].GetComponent<FadeControl>().StartFadeIn();
            }
            else
            {
                //runes_summoned[i].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                //runes_summoned[i].GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0);
                runes_summoned[i].GetComponent<Rune>().SetWrong(i);
                //runes_summoned[i].GetComponent<FadeControl>().StartFadeIn();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShowRequirement()
    {
        if (block_selection.active == false)
        {
            detail_requirement.SetActive(true);
            DarkLayer = Instantiate(DarkLayerInstance, transform);
            DarkLayer.transform.localPosition = new Vector3(0, 0, DarkLayer.transform.localPosition.z);
            DarkLayer.GetComponent<FadeControl>().StartFadeIn();
        }
    }

    public void CloseRequirement()
    {
        //Debug.Log("here!");
        StartCoroutine(CloseRequirementRoutine());
    }

    IEnumerator CloseRequirementRoutine()
    {
        //Debug.Log("wohoo!");
        DarkLayer.GetComponent<FadeControl>().StartFadeOut();
        detail_requirement.GetComponent<Animator>().enabled = false;
        detail_requirement.GetComponent<MoveTo>().ReplayMotion();
        Destroy(DarkLayer, 0.5f);
        
        yield return new WaitForSeconds(0.5f);
        detail_requirement.SetActive(false);
        detail_requirement.GetComponent<Animator>().enabled = true;
    }

    public void SetDebugStep(int step)
    {
        //int step = (int) (slide_value * debug_space.current_step);
        step_indicator.GetComponent<StepIndicator>().SetStepText(step, debug_space[current_space_index].TotalStep());
    }

    public string GetDebugText(int step)
    {
        string debugText = "Expected Output: " + debug_expected_output[current_space_index] + "          " + "Your Output: " + debug_real_output[current_space_index] + "\n";
        debugText = debugText + debug_space[current_space_index].DebugTextAtStep(step);
        return debugText;
    }

    [ContextMenu("DebugRunCode")]
    public void DebugRunCode()
    {
        GetAllCommand();
        ExecutionSpace exec = new ExecutionSpace();
        string output = exec.StartExecution(coding_blocks, "");
        if (output.Length > 0)
        {
            Debug.Log("The program output: " + output);
        }
        else
        {
            Debug.Log("The program output nothing.");
        }
    }

    public void OpenBlockSelection()
    {
        if (block_selection.active == false)
        {
            block_selection.SetActive(true);
            block_selection.GetComponent<Collider2D>().enabled = true;
            DarkLayer = Instantiate(DarkLayerInstance, transform);
            DarkLayer.transform.localPosition = new Vector3(0, 0, DarkLayer.transform.localPosition.z);
            DarkLayer.GetComponent<FadeControl>().StartFadeIn();
        }
    }

    public void CloseBlockSelection()
    {
        StartCoroutine(CloseBlockSelectionRoutine());
    }

    IEnumerator CloseBlockSelectionRoutine()
    {
        //block_selection.GetComponent<FadeControl>().GetAllComponenets();
        DarkLayer.GetComponent<FadeControl>().StartFadeOut();
        block_selection.GetComponent<BlockSelection>().FadeAllBlocks();
        block_selection.GetComponent<Collider2D>().enabled = false;
        Destroy(DarkLayer, 0.5f);
        block_selection.GetComponent<FadeControl>().StartFadeOut();
        block_selection.GetComponent<ScaleChange>().StartAnimateReverse();
        yield return new WaitForSeconds(0.5f);
        block_selection.SetActive(false);
    }

    public void DisableScrolling()
    {
        coding_scroll_list.GetComponent<ScrollRect>().vertical = false;
        coding_scroll_list.GetComponent<ScrollRect>().horizontal = false;
    }

    public void EnableScrolling()
    {
        coding_scroll_list.GetComponent<ScrollRect>().vertical = true;
        coding_scroll_list.GetComponent<ScrollRect>().horizontal = true;
    }

    public void GenerateInputBlock(string variable_name)
    {

    }

    public void RaiseCurse()
    {
        //GameObject temp = Instantiate(curse, transform);
        //Destroy(temp, 2f);
        cursed = true;
    }

    [ContextMenu("FinishQuest")]
    public void FinishQuest()
    {
        Debug.Log("Quest " + questID + " finished!");
        if(QuestManager.Instance.getQuestFromID(questID).skill_name != null && QuestManager.Instance.getQuestFromID(questID).skill_name != "")
        {
            GetAllCommand();
            string skill_name = QuestManager.Instance.getQuestFromID(questID).skill_name;
            int input_slots = QuestManager.Instance.getQuestFromID(questID).input_slots_length;
            Skill new_skill = new Skill(coding_blocks, skill_name, input_slots);
            int total_step = 0;
            foreach(ExecutionSpace space in debug_space)
            {
                total_step += space.TotalStep();
            }
            total_step /= debug_space.Count;
            new_skill.average_steps = total_step;
            if(GameManager.Instance.GetSkillByName(skill_name) != null)
            {
                Player.Instance.data.skills.Remove(GameManager.Instance.GetSkillByName(skill_name));
            }
            Player.Instance.data.skills.Add(new_skill);
        }
        StartCoroutine(GameManager.Instance.FinishMission(questID));
    }

    public void ResetCodingUI()
    {
        foreach (GameObject rune in runes_summoned)
        {
            Destroy(rune);
        }
        runes_summoned.Clear();
        debug_space.Clear();
        passed.Clear();
        debug_real_output.Clear();
        debug_expected_output.Clear();

        playbar.SwitchToCode();
        main_code_area.ResetCodeArea();
        coding_blocks.Clear();
        quest_inputs.Clear();
        expect_outputs.Clear();
        finish_button.SetActive(false);
        add_icon.GetComponent<SpriteRenderer>().sprite = add;
        add_icon.GetComponent<ChildButton>().ButtonUse = "OpenBlockSelection";
        coding_area.SetActive(true);
        cursed = false;
        toolong = false;
        zerodivision = false;
    }
}
