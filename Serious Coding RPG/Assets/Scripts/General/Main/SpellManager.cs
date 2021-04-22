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
        if (black_mist != null)
        {
            //Time.timeScale = 0;

            BattleManager.Instance.paused = true;
            string spell_name = spell.required_skill;
            Debug.Log(spell_name);
            BattleManager.Instance.RepairSpell(spell_name, gameObject);
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
