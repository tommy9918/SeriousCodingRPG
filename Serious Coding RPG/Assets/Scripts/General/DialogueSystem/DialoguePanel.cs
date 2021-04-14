using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialoguePanel : MonoBehaviour
{
    public static DialoguePanel Instance { get; private set; }
    public List<string> dialogues;
    public int current_dialogue_index;
    public int current_sentence_index;
    public string current_text;
    public bool canNext;
    public bool started;
    public bool finish;
    public int text_scroll_speed;
    public int current;
    public string NPCName;
    public string questID;
    public Text dialogue_text;
    public Text NPC_name_text;
    public GameObject NPC_avatar;
    public GameObject NPC;
    public GameObject next_icon;
    public GameObject cam;
    public GameObject darken;
    public GameObject panel;

    void Awake()
    {
        if (Instance == null)   //singleton Dialogue Panel instance, easy for referencing in other scripts
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        panel.GetComponent<BoxCollider2D>().enabled = false;
        next_icon.SetActive(false);
        darken.GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
        if (started && !finish)
        {
            if (!canNext)
            {
                if (current == text_scroll_speed)
                {
                    current_sentence_index++;
                    dialogue_text.text = "<b>" + SentenceInProgress(dialogues[current_dialogue_index], current_sentence_index) + "</b>";

                    if (current_sentence_index >= dialogues[current_dialogue_index].Length)
                    {
                        canNext = true;
                        next_icon.SetActive(true);
                    }
                    current = 0;
                }
                else
                {
                    current++;
                }
            }
        }
    }

    public void StartDialogue(GameObject npc)
    {
        this.NPC = npc;
        NPCManager manager = npc.GetComponent<NPCManager>();
        NPC_avatar.GetComponent<SpriteRenderer>().sprite = npc.GetComponent<SpriteRenderer>().sprite;
        questID = manager.quest_id;
        dialogues = npc.GetComponent<DialogueManager>().dialogues;
        NPCName = LoadText("NPCNAME"+manager.NPC_id);
        current_dialogue_index = 0;
        current_sentence_index = 0;
        NPC_name_text.text = "<b>" + NPCName + "</b>";
        dialogue_text.text = "";
        current = 0;
        canNext = false;
        finish = false;
        started = true;
        darken.GetComponent<BoxCollider2D>().enabled = true;
        panel.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.SetActive(true);
        
        GetComponent<FadeControl>().StartFadeIn();

    }

    string SentenceInProgress(string sentence, int progress)
    {
        return sentence.Substring(0, progress) + "<color=#00000000>" + sentence.Substring(progress, sentence.Length-progress) + "</color>";
    }

    

    public void Next()
    {
        //Debug.Log("next");
        if (!canNext)
        {
            current_sentence_index = dialogues[current_dialogue_index].Length - 1;
        }
        else
        {
            current_dialogue_index++;
            current_sentence_index = 0;
            current = 0;
            canNext = false;
            next_icon.SetActive(false);
            if (current_dialogue_index >= dialogues.Count)
            {
                StartCoroutine(Finish());
            }
        }
    }

    IEnumerator Finish()
    {
        //Debug.Log(finish);
        started = false;
        finish = true;
        panel.GetComponent<BoxCollider2D>().enabled = false;
        darken.GetComponent<BoxCollider2D>().enabled = false;
        next_icon.SetActive(false);
        GetComponent<FadeControl>().StartFadeOut();
        if (questID != null) QuestManager.Instance.StartQuest(questID);
        yield return new WaitForSeconds(GetComponent<FadeControl>().speed/60f + 5);
        //gameObject.SetActive(false);

    }

    public string LoadText(string target_key)
    {
        List<string> loaded_dialogues = new List<string>();

        string lang = Player.Instance.data.language;

        string allTexts = (Resources.Load("TextContents/Dialogues/" + lang) as TextAsset).text; //without (.txt)
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        string key, value;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                    lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);  //get string after '='
                if (CompareKeys(target_key, key))
                {
                    return value;
                }
            }
        }

        return null;
    }

    bool CompareKeys(string search_key, string read_key)
    {
        //Debug.Log(search_key);
        //Debug.Log(read_key);

        for (int i = 0; i <= search_key.Length - 1; i++)
        {
            if (search_key[i] != read_key[i]) return false;
        }
        return true;
    }


}
