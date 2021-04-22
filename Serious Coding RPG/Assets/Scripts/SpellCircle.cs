using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCircle : MonoBehaviour
{
    public Image circle;
    public int speed;
    public List<int> spell_lengths;
    public float fill;
    public int current_index;
    int executing;
    public GameObject list;

    // Start is called before the first frame update
    void Start()
    {
        current_index = 0;
        fill = 0f;
        circle.fillAmount = 0f;
        executing = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BattleManager.Instance.paused)
        {
            fill += Time.deltaTime * speed / spell_lengths[current_index] * executing;
            circle.fillAmount = fill;
            if (fill >= 1f)
            {
                fill = 0f;
                current_index = (current_index + 1) % spell_lengths.Count;
                executing = 0;
                list.GetComponent<SpellList>().GoNextSpell();
                StartCoroutine(CastSpellSequence());
            }
        }
    }

    IEnumerator CastSpellSequence()
    {              
        yield return new WaitForSeconds(0.2f);
        executing = 1;
    }
}
