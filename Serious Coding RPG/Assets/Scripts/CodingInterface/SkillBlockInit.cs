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
        //skill = Player.Instance.data.skills[0];
        skill_text.text = skill.name.ToLower();
        for(int i = 0; i <= skill.number_of_slots - 2; i++)
        {
            GetComponent<SubBlockManager>().block_sites.Add(Instantiate(slots_ref, gameObject.transform));
        }
    }
}
