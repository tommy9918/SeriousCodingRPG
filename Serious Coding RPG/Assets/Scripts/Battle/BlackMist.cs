using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMist : MonoBehaviour
{
    void OnTouchUp()
    {
        //Time.timeScale = 0;
        
        BattleManager.Instance.paused = true;
        string spell_name = gameObject.transform.parent.gameObject.GetComponent<SpellManager>().spell.required_skill;
        Debug.Log(spell_name);
        BattleManager.Instance.RepairSpell(spell_name, gameObject);
    }

    public void RepairSuccess()
    {
        Destroy(gameObject);
    }
}
