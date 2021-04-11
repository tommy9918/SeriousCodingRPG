using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRepair : MonoBehaviour
{
    public GameObject repairing_spell;
    public GameObject block_parent;
    
    void OnTouchUp()
    {
        BattleManager.Instance.repairing_spell.GetComponent<SpellManager>().RepairSuccess();
        gameObject.SetActive(false);
        foreach (Transform child in block_parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
