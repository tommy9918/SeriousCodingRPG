using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardPanel : MonoBehaviour
{
    public string questID;
    public int EXP;
    public int gold;
    public Text EXP_text;
    public Text gold_text;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ScaleChange>().StartAnimate();
        Quest quest = QuestManager.Instance.getQuestFromID(questID);
        EXP = quest.rewardEXP;
        gold = quest.rewardGold;
        EXP_text.text = EXP.ToString();
        gold_text.text = gold.ToString();
    }

    void ClickedYes()
    {
        GetComponent<ScaleChange>().StartAnimateReverse();
        Player.Instance.data.EXP += EXP;
        Player.Instance.data.gold += gold;
        Destroy(gameObject, 1f);
    }

    
}
