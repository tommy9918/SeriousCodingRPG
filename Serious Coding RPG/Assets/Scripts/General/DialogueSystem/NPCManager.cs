using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject speechBubble;
    public List<GameObject> interact_button;
    public GameObject quest_icon;
    public DialogueManager dialogue_manager;
    public float react_radius;
    public bool canTalkTo;
    public string NPC_id;
    public string quest_id;
    //public string NPCname;

    // Start is called before the first frame update
    void Start()
    {
        dialogue_manager = GetComponent<DialogueManager>();
        UpdateQuest();
    }

    public void UpdateQuest()
    {
        quest_id = QuestManager.Instance.getQuest(NPC_id);
        if (quest_id != null)
        {
            dialogue_manager.dialogues = dialogue_manager.LoadQuestDialogues(quest_id);
            quest_icon.SetActive(true);
        }
        else
        {
            dialogue_manager.dialogues = dialogue_manager.LoadDialogues(NPC_id);
            quest_icon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, Player.Instance.character.transform.position);
        if(dist < react_radius)
        {
            SetBubbleOn();
        }
        else
        {
            SetBubbleOff();
        }
    }

    void SetBubbleOn()
    {
        if (!canTalkTo)
        {
            canTalkTo = true;
            //speechBubble.GetComponent<ScaleChange>().StartAnimate(); 
            foreach(GameObject ib in interact_button)
            {
                ib.GetComponent<ScaleChange>().StartAnimate();
            }
        }
        else
        {
            //speechBubble.GetComponent<BoxCollider2D>().enabled = true;
            foreach (GameObject ib in interact_button)
            {
                ib.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void SetBubbleOff()
    {
        if (canTalkTo)
        {
            canTalkTo = false;
            //speechBubble.GetComponent<BoxCollider2D>().enabled = false;
            //speechBubble.GetComponent<ScaleChange>().StartAnimateReverse();
            foreach (GameObject ib in interact_button)
            {
                ib.GetComponent<BoxCollider2D>().enabled = false;
                ib.GetComponent<ScaleChange>().StartAnimateReverse();
            }

        }
    }

    public void StartDialogue()
    {
        DialoguePanel.Instance.StartDialogue(gameObject);
    }
}
