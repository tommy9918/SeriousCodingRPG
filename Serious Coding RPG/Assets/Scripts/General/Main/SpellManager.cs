using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public BattleSpell spell;
    public SpriteRenderer spell_sprite_ren;
    public Text spell_name;
    public GameObject name_part;
    public GameObject black_mist;
    public bool in_battle;
    public bool can_expand_information;
    public bool can_buy;
    public GameObject detail_panel;

    [ContextMenu("InitializeSpell")]
    public void InitializeSpell()
    {
        spell_sprite_ren.sprite = spell.sprite;
        spell_name.text = MasterTextManager.Instance.LoadText(spell.spell_id).ToUpper();
    }

    [ContextMenu("EnableNamePart")]
    public void EnableNamePart()
    {
        name_part.SetActive(true);
    }

    [ContextMenu("DisableNamePart")]
    public void DisableNamePart()
    {
        name_part.SetActive(false);
    }

    void OnTouchUp()
    {
        if (in_battle && black_mist != null)
        {
            //Time.timeScale = 0;

            BattleManager.Instance.paused = true;
            string spell_name = spell.required_skill;
            Debug.Log(spell_name);
            BattleManager.Instance.RepairSpell(spell_name, gameObject);
        }
        else if (can_buy)
        {
            GameObject temp = Instantiate(detail_panel, Player.Instance.transform.position, Quaternion.identity);
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, detail_panel.transform.position.z);
            temp.GetComponent<SpellDetail>().buyable = true;
            temp.GetComponent<SpellDetail>().InitializeSpellDetail(spell);
            //PopUpManager.Instance.OpenComplexPopUp(temp);
        }
        else if (can_expand_information)
        {
            GameObject temp = Instantiate(detail_panel, Player.Instance.transform.position, Quaternion.identity);
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, detail_panel.transform.position.z);
            //temp.GetComponent<SpellDetail>().buyable = true;
            temp.GetComponent<SpellDetail>().InitializeSpellDetail(spell);
            //PopUpManager.Instance.OpenComplexPopUp(temp);
        }
    }

    public void RepairSuccess()
    {
        BattleManager.Instance.paused = false;
        Destroy(black_mist);
    }

    public void RepairFail()
    {
        BattleManager.Instance.paused = false;
        //Destroy(black_mist);
        GetComponent<Collider2D>().enabled = false;
    }
}
