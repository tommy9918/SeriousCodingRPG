using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCircle : MonoBehaviour
{
    public Image circle;
    public int channel_index;
    public int speed;
    public List<int> spell_lengths;
    public float fill;
    public int current_index;
    bool executing;
    public GameObject list;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!BattleManager.Instance.paused && executing)
        {
            fill += Time.deltaTime * speed / spell_lengths[current_index];
            circle.fillAmount = fill;
            if (fill >= 1f)
            {
                fill = 0f;
                current_index = (current_index + 1) % spell_lengths.Count;
                executing = false;
                list.GetComponent<SpellList>().GoNextSpell();
                StartCoroutine(CastSpellSequence());
            }
        }
    }

    public void InitializeSpellCircle(int index)
    {
        //Debug.Log("here");
        channel_index = index;
        spell_lengths = new List<int>();
        speed = Player.Instance.data.channel_chanting_speed[index];
        for(int i = 0;i <= Player.Instance.data.spells_in_channels[index].spell_list.Count - 1; i++)
        {
            spell_lengths.Add(Player.Instance.data.spells_in_channels[index].spell_list[i].average_steps);
        }
        current_index = 0;
        fill = 0f;
        circle.fillAmount = 0f;
        executing = true;
        //Debug.Log("success");
    }

    IEnumerator CastSpellSequence()
    {              
        yield return new WaitForSeconds(0.5f);
        executing = true;
    }
}
