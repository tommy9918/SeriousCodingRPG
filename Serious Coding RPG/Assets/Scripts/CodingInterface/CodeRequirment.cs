using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeRequirment : MonoBehaviour
{
    public Text QuestID;
    public Text QuestName;

    public Text description_title;
    public Text description;

    public Text example_title;
    public Text example;

    public void SetRequirementText(string questID)
    {
        QuestID.text = MasterTextManager.Instance.LoadText("QUEST")+" "+questID+":";
        description_title.text = MasterTextManager.Instance.LoadText("DESCRIPTION");
        example_title.text = MasterTextManager.Instance.LoadText("EXAMPLE");

        QuestName.text = MasterTextManager.Instance.LoadQuestText("QUEST" + questID + "_NAME");
        description.text = "<b><i>" + MasterTextManager.Instance.LoadQuestText("QUEST" + questID + "_DESCRIPTION") + "</i></b>";
        example.text = "<b><i>" + ExampleText(questID) + "</i></b>";
    }

    string ExampleText(string questID)
    {
        Quest current_quest = QuestManager.Instance.getQuestFromID(questID);
        string exampleText = "";
        if (current_quest.input[0].Length > 0)
        {
            exampleText += MasterTextManager.Instance.LoadText("INPUT");
            exampleText += string.Join(", ", current_quest.input[0].Split(','));
            exampleText += "\n";
        }
        exampleText += MasterTextManager.Instance.LoadText("OUTPUT");
        exampleText += current_quest.output[0];

        return exampleText;

        //return "";
    }
}
