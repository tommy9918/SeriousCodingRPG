using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static QuestManager Instance { get; private set; }
    public Quest[] all_quests;
    public NPCManager[] NPC_list;
    public GameObject QuestConfirmPanel;
    public GameObject questUI;
    public GameObject RewardPanel;

    void Awake()
    {
        if (Instance == null)   //singleton Quest instance, easy for referencing in other scripts
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadAllQuests();   
    }

    void LoadAllQuests()
    {
        string path = "ScriptableObjects/Quest";
        all_quests =  Resources.LoadAll<Quest>(path);
        //Debug.Log(Resources.LoadAll(path, typeof(Quest)).Length);
    }

    public string getQuest(string NPCid)
    {
        //Debug.Log(Player.Instance);
        if (all_quests == null) return null;
        foreach(Quest quest in all_quests)
        {
            
            if(string.Compare(NPCid, quest.questNPCId) == 0 && !Player.Instance.checkQuestStatus(int.Parse(quest.questId)))
            {
                
                return quest.questId;
            }
        }
        return null;
    }

    public void StartQuest(string questID)
    {
        //Debug.Log("Start Quest " + questID);
        Vector3 pos = transform.position;
        GameObject temp = Instantiate(QuestConfirmPanel, new Vector3(pos.x, pos.y, QuestConfirmPanel.transform.position.z), Quaternion.identity);
        temp.GetComponent<QuestConfirmPanel>().questID = questID;
    }

    public void QuestUIStart(string questID)
    {
        Vector3 pos = transform.position;
        GameObject temp = Instantiate(questUI, new Vector3(pos.x, pos.y, questUI.transform.position.z), Quaternion.identity);
        temp.GetComponent<QuestUI>().questID = questID;
    }

    public void FinishQuest(string questID)
    {
        int id = int.Parse(questID);
        Player.Instance.data.completedTask.Add(id);
        Vector3 pos = transform.position;
        GameObject temp = Instantiate(RewardPanel, new Vector3(pos.x, pos.y, questUI.transform.position.z), Quaternion.identity);
        temp.GetComponent<QuestRewardPanel>().questID = questID;
        foreach(NPCManager manager in NPC_list)
        {
            manager.UpdateQuest();
        }
    }

    public Quest getQuestFromID(string questID)
    {
        foreach(Quest quest in all_quests)
        {
            if (questID.Equals(quest.questId)) return quest;
        }
        return null;
    }
}
