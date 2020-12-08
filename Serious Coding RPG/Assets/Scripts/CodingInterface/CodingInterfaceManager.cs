using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingInterfaceManager : MonoBehaviour
{
    public string questID;

    public GameObject block_selection;
    public GameObject block_selection_scroll_list;
    public GameObject coding_scroll_list;
    public GameObject DarkLayerInstance;
    public GameObject DarkLayer;
    public GameObject detail_requirement;
    public MainCodeArea main_code_area;
    
    public List<CommandBlock> coding_blocks;
    public List<string> quest_inputs;
    public List<string> expect_outputs;

    public GameObject step_indicator;
    public DebugBar debug_bar;
    

    public GameObject active_dragging_block;

    public ExecutionSpace debug_space;
    public string debug_expected_output;
    public string debug_real_output;

    // Start is called before the first frame update
    void Start()
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
    }

    [ContextMenu("RunCode")]
    public void RunCode()
    {
        //Debug.Log(true.ToString());
        GetAllCommand();
        for (int i = 0; i <= quest_inputs.Count - 1; i++)
        {
            ExecutionSpace exec = new ExecutionSpace();
            string output = exec.StartExecution(coding_blocks, quest_inputs[i]);
            if (output.Length > 0)
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
                debug_space = exec;
                debug_expected_output = expect_outputs[i];
                debug_real_output = output;
                step_indicator.GetComponent<StepIndicator>().Summon(1, debug_space.current_step);
            }
            else
            {
                //Debug.Log("Expected output is: " + expect_outputs[i]);
                Debug.Log("Test case failed! Try again?");
                debug_space = exec;
                debug_expected_output = expect_outputs[i];
                debug_real_output = output;
                step_indicator.GetComponent<StepIndicator>().Summon(1, debug_space.current_step);
                debug_bar.SetWrong();
                break;
            }
        }
    }

    public void ShowRequirement()
    {
        detail_requirement.SetActive(true);
        DarkLayer = Instantiate(DarkLayerInstance);
        DarkLayer.GetComponent<FadeControl>().StartFadeIn();
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
        step_indicator.GetComponent<StepIndicator>().SetStepText(step, debug_space.current_step);
    }

    public string GetDebugText(int step)
    {
        string debugText = "Expected Output: " + debug_expected_output + "          " + "Your Output: " + debug_real_output + "\n";
        debugText = debugText + debug_space.DebugTextAtStep(step);
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
            DarkLayer = Instantiate(DarkLayerInstance);
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
        Destroy(DarkLayer, 0.5f);
        block_selection.GetComponent<FadeControl>().StartFadeOut();
        block_selection.GetComponent<ScaleChange>().StartAnimateReverse();
        yield return new WaitForSeconds(0.5f);
        block_selection.SetActive(false);
    }

    public void DisableScrolling()
    {
        coding_scroll_list.GetComponent<ScrollRect>().vertical = false;
    }

    public void EnableScrolling()
    {
        coding_scroll_list.GetComponent<ScrollRect>().vertical = true;
    }

    public void GenerateInputBlock(string variable_name)
    {

    }
}
