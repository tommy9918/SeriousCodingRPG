using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingInterfaceManager : MonoBehaviour
{
    public string questID;

    public GameObject block_selection;
    public GameObject block_selection_scroll_list;
    public GameObject coding_scroll_list;
    public List<GameObject> coding_blocks;

    public GameObject active_dragging_block;

    // Start is called before the first frame update
    void Start()
    {
        coding_blocks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBlockSelection()
    {
        block_selection.SetActive(true);
    }

    public void CloseBlockSelection()
    {
        StartCoroutine(CloseBlockSelectionRoutine());
    }

    IEnumerator CloseBlockSelectionRoutine()
    {
        //block_selection.GetComponent<FadeControl>().GetAllComponenets();
        block_selection.GetComponent<FadeControl>().StartFadeOut();
        block_selection.GetComponent<ScaleChange>().StartAnimateReverse();
        yield return new WaitForSeconds(0.5f);
        block_selection.SetActive(false);
    }

    public void GenerateInputBlock(string variable_name)
    {

    }
}
