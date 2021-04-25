using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpell : MonoBehaviour
{
    public GameObject spell_parent;
    public GameObject spell_ref;
    public int index;
    public List<GameObject> spell_list;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimate()
    {
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
        Destroy(gameObject, 0.5f);
    }

    [ContextMenu("InitializeAddSpellWindow")]
    public void InitializeAddSpellWindow()
    {
        spell_list = new List<GameObject>();
        List<BattleSpell> unequip = GameManager.Instance.GetUnequippedSpell();
        for(int i = 0; i <= unequip.Count - 1 ; i++)
        {
            GameObject temp = Instantiate(spell_ref, spell_parent.transform);
            temp.transform.localPosition = new Vector3(3.45f + 2.39f * (i % 2), -4.48f - 2.2f * (i / 2), -0.5f);
            temp.GetComponent<SpellManager>().spell = unequip[i];
            temp.GetComponent<SpellManager>().DisableNamePart();
            temp.GetComponent<SpellManager>().InitializeSpell();
            temp.GetComponent<SpellManager>().can_add = true;
            temp.GetComponent<SpellManager>().add_spell_controler = this;
            temp.GetComponent<SetMaskInteration>().SetInteraction("inside");
            spell_list.Add(temp);
        }

        StartAnimate();
    }

    public void Add(string spell_id)
    {
        Player.Instance.data.spells_in_channels[index].spell_list.Add(GameManager.Instance.FindPlayerSpellBySpellID(spell_id));
        SpellOrganise.Instance.Refresh();
        CloseWindow();
    }
}
