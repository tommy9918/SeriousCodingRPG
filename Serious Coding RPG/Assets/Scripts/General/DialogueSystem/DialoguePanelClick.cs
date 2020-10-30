using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanelClick : MonoBehaviour
{
    public GameObject parent;
    

    void OnTouchUp()
    {
        parent.GetComponent<DialoguePanel>().Next();
    }
}
