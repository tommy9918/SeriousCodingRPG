using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MasterTextManager : MonoBehaviour
{
    public static MasterTextManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)   //singleton Player instance, easy for referencing in other scripts
        {
            Instance = this;          
        }
        
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

        return "";
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
