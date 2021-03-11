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
}
