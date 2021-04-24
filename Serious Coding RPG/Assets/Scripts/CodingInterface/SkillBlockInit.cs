using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBlockInit : MonoBehaviour
{
    public Skill skill;
    public Text skill_text;
    public GameObject slots_ref;

    [ContextMenu("Init")]
    public void InitializeSkillBlock()
    {
        //Debug.Log("Hi!");
        GetComponent<SubBlockManager>().block_sites = new List<GameObject>();
        GetComponent<SubBlockManager>().block_sites.Add(skill_text.gameObject);
        //skill = Player.Instance.data.skills[0];
        skill_text.text = skill.name.ToLower();
        GetComponent<SubBlockManager>().skill_name_text = skill_text;
        for (int i = 0; i <= skill.number_of_slots - 1; i++)
        {
            GetComponent<SubBlockManager>().block_sites.Add(Instantiate(slots_ref, gameObject.transform));
        }
        GetComponent<SubBlockManager>().SetSkillBlockPosition();
    }
}
