using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOrganise : MonoBehaviour
{
    public static SpellOrganise Instance;
    public GameObject skill_ref;

    public List<GameObject> learned_skills;
    public GameObject learned_parent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeOrganisePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 0.6f);
    }

    public void Refresh()
    {
        
        foreach (GameObject obj in learned_skills)
        {
            Destroy(obj);
        }
        InitializeOrganisePanel();
    }

    [ContextMenu("InitializeOrganisePanel")]
    public void InitializeOrganisePanel()
    {
        
        learned_skills = new List<GameObject>();

       
        foreach (BattleSpell spell in GameManager.Instance.GetLearntSpell())
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
            learned_skills[i].transform.localPosition = new Vector3(1.11f + (i % 4) * 1.9f, -4.25f - (i / 4) * -2f, -1f);
        }
    }
}
