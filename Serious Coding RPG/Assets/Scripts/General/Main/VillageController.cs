using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageController : MonoBehaviour
{
    public string VillageID;
    public GameObject objects;
    public List<GameObject> NPC_list;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeVillage()
    {
        objects.GetComponent<SetObjectsZ>().SetZOrder();
        QuestManager.Instance.NPC_list = new List<NPCManager>();
        foreach (GameObject npc in NPC_list)
        {
            npc.GetComponent<SetZ>().UpdateZ();
            QuestManager.Instance.NPC_list.Add(npc.GetComponent<NPCManager>());
        }
        character.GetComponent<CharacterManager>().InitializeCharacter();
    }
}

