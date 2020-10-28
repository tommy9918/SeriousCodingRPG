﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    List<string> dialogues;
    public string dialogue_target;
    // Start is called before the first frame update
    void Start()
    {
        dialogues = LoadDialogues(dialogue_target);
        PrintAllDialogue();
    }

    

    List<string> LoadDialogues(string target_key)
    {
        List<string> loaded_dialogues = new List<string>();

        string lang = LanaguageFileTarget(SystemLanguage.English);

        string allTexts = (Resources.Load("TextContents/Dialogues/"+lang) as TextAsset).text; //without (.txt)
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
                    loaded_dialogues.Add(value);
                }
            }
        }

        return loaded_dialogues;
    }

    void PrintAllDialogue()
    {
        for(int i = 0; i <= dialogues.Count - 1; i++)
        {
            Debug.Log(dialogues[i]);
        }
    }

    bool CompareKeys(string search_key, string read_key)
    {
        bool match = true;
        for(int i = 0; i <= search_key.Length - 1; i++)
        {
            if (search_key[i] != read_key[i]) match = false;
        }
        return match;
    }

    string LanaguageFileTarget(SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.English:
                return "en";
            case SystemLanguage.Chinese:
                return "ch";
        }
        return "en";
    }
}