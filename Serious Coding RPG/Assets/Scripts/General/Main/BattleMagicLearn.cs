using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicLearn : MonoBehaviour
{
    public GameObject skill_ref;
    public List<GameObject> buyable_skills;
    public List<GameObject> learned_skills;
    public GameObject buyable_parent;
    public GameObject learned_parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("InitializeLearningPanel")]
    public void InitializeLearningPanel()
    {
        buyable_skills = new List<GameObject>();
        learned_skills = new List<GameObject>();

        foreach(BattleSpell spell in GameManager.Instance.GetBuyableSpell())
        {
            GameObject temp = Instantiate(skill_ref, buyable_parent.transform);
            temp.GetComponent<SpellManager>().spell = spell;
            temp.GetComponent<SpellManager>().DisableNamePart();
            temp.GetComponent<SpellManager>().InitializeSpell();
            temp.GetComponent<SpellManager>().can_buy = true;
            buyable_skills.Add(temp);

        }
        for(int i = 0; i <= buyable_skills.Count - 1; i++)
        {
            buyable_skills[i].transform.localPosition = new Vector3(1.11f + (i%4)*1.9f, -3.53f - (i/4)*-2f, -1f);
        }

        foreach(BattleSpell spell in GameManager.Instance.GetLearntSpell())
        {
            GameObject temp = Instantiate(skill_ref, learned_parent.transform);
            temp.GetComponent<SpellManager>().spell = spell;
            temp.GetComponent<SpellManager>().DisableNamePart();
            temp.GetComponent<SpellManager>().InitializeSpell();
            temp.GetComponent<SpellManager>().can_expand_information = true;
            learned_skills.Add(temp);
        }

        for (int i = 0; i <= learned_skills.Count - 1; i++)
        {
            learned_skills[i].transform.localPosition = new Vector3(1.11f + (i % 4) * 1.9f, -3.53f - (i / 4) * -2f, -1f);
        }
    }
}
