using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestConfirmPanel : MonoBehaviour
{
    
    public string questID;

    public Text QuestName;
    public Text description;
    public Text reward_title;
    public Text reward;

    public Text accept;
    public Text decline;


    // Start is called before the first frame update
    void Start()
    {
        QuestName.text = MasterTextManager.Instance.LoadQuestText("QUEST" + questID + "_NAME").ToUpper();
        description.text = "<b><i>" + MasterTextManager.Instance.LoadQuestText("QUEST" + questID + "_DESCRIPTION") + "</i></b>";
        reward_title.text = MasterTextManager.Instance.LoadText("REWARD").ToUpper();
        reward.text = RewardText(questID);

        accept.text = MasterTextManager.Instance.LoadText("ACCEPT").ToUpper();
        decline.text = MasterTextManager.Instance.LoadText("DECLINE").ToUpper();

        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<FadeControl>().StartFadeIn();

    }

    string RewardText(string questID)
    {
        Quest current_quest = QuestManager.Instance.getQuestFromID(questID);
        string rewardText = "<b>";
        if(current_quest.rewardEXP > 0)
        {
            rewardText += MasterTextManager.Instance.LoadText("EXP") + ": " + current_quest.rewardEXP.ToString() + "\n";
        }
        if (current_quest.rewardGold > 0)
        {
            rewardText += MasterTextManager.Instance.LoadText("GOLD") + ": " + current_quest.rewardGold.ToString() + "\n";
        }
        if(current_quest.skill_name != null && current_quest.skill_name != "")
        {
            
            rewardText += MasterTextManager.Instance.LoadText("NEWSKILL") + ": " + current_quest.skill_name + "\n";
        }
        rewardText += "</b>";
        return rewardText;
    }

    void ClickedYes()
    {
        //Debug.Log("Yes");
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<FadeControl>().StartFadeOut();
        Destroy(gameObject, 1f);
        QuestManager.Instance.QuestUIStart(questID);
        PopUpManager.Instance.RemoveDim();
        //start quest with quest id
    }

    void ClickedNo()
    {
        //Debug.Log("No");
        GetComponent<ScaleChange>().StartAnimateReverse();
        PopUpManager.Instance.RemoveDim();
        Destroy(gameObject, 1f);
    }

    

    


}
