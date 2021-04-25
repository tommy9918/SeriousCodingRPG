using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDetail : MonoBehaviour
{
    public SpellManager spell;
    public SkillBlockInit skill_blk;
    public Text spell_description;
    public Text spell_length;
    public Text spell_effect;
    public Text spell_price;
    public GameObject buy_button;
    public bool buyable;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
        Destroy(gameObject, 0.6f);
    }

    public void BuySpell()
    {
        int price = spell.spell.price;
        if (Player.Instance.data.gold >= price)
        {
            Player.Instance.data.gold -= price;
            int step = GameManager.Instance.GetSkillByName(spell.spell.required_skill).average_steps;
            int memory = GameManager.Instance.GetSkillByName(spell.spell.required_skill).average_memory;
            Player.Instance.data.all_spells.Add(new PlayerSpell(step, memory, spell.spell.spell_id));
            CloseWindow();
            BattleMagicLearn.Instance.Refresh();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    
    public void InitializeSpellDetail(BattleSpell spell_info)
    {
        spell.spell = spell_info;
        spell.InitializeSpell();
        spell.EnableNamePart();
        skill_blk.skill = GameManager.Instance.GetSkillByName(spell_info.required_skill);
        skill_blk.InitializeSkillBlock();

        spell_description.text = "<b><i>" + MasterTextManager.Instance.LoadText(spell_info.spell_id + "_DESCRIPTION") + "</i></b>";
        spell_length.text = MasterTextManager.Instance.LoadText("AVERAGELENGTH") + ": " + skill_blk.skill.average_steps;
        spell_effect.text = effect_text(spell_info.usage, spell_info.effect_value.ToString());
        spell_price.text = spell_info.price.ToString() + " " + MasterTextManager.Instance.LoadText("GOLD").ToUpper();
        if (buyable)
        {
            buy_button.SetActive(true);
        }
        else
        {
            buy_button.SetActive(false);
        }

    }

    public string effect_text(string type, string value)
    {
        string final_text = MasterTextManager.Instance.LoadText(type.ToUpper());
        final_text = final_text.Replace("<value>", value);
        return final_text;
    }

    
}
