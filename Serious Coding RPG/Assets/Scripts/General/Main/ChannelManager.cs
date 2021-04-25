using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelManager : MonoBehaviour
{
    public int channel_index;
    public GameObject spell_ref;
    public GameObject add_icon;
    public GameObject spell_parent;
    public Text channel_speed;

    public GameObject add_spell_window;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeChannel(int index)
    {
        channel_index = index;
        for(int i = 0; i <= Player.Instance.data.spells_in_channels[index].spell_list.Count - 1; i++)
        {
            GameObject temp = Instantiate(spell_ref, new Vector3(2.21f + 3.78f*i, -2.198f, -1.51f), Quaternion.identity, spell_parent.transform);
            temp.transform.localPosition = new Vector3(2.21f + 3.78f * i, -2.198f, -1.51f);
            temp.GetComponent<SpellManager>().spell = GameManager.Instance.GetSpellBySpellID(Player.Instance.data.spells_in_channels[index].spell_list[i].spell_id);
            temp.GetComponent<SpellManager>().InitializeSpell();
            temp.GetComponent<SpellManager>().DisableNamePart();
            temp.GetComponent<SpellManager>().can_delete = true;
            temp.GetComponent<SpellManager>().channel_manager = this;
            temp.transform.localScale = new Vector3(2, 2, 1);
            if (i == Player.Instance.data.spells_in_channels[index].spell_list.Count - 1 && GameManager.Instance.GetUnequippedSpell().Count >= 1)
            {
                GameObject add = Instantiate(add_icon, new Vector3(2.21f + 3.78f * (i+1), -2.198f, -1.51f), Quaternion.identity, spell_parent.transform);
                add.transform.localPosition = new Vector3(2.21f + 3.78f * (i + 1), -2.198f, -1.51f);
                add.GetComponent<ChildButton>().parent = gameObject;
                add.GetComponent<ChildButton>().ButtonUse = "AddSpell";

            }

        }
        if(Player.Instance.data.spells_in_channels[index].spell_list.Count == 0 && GameManager.Instance.GetUnequippedSpell().Count >= 1)
        {
            GameObject add = Instantiate(add_icon, new Vector3(2.21f, -2.198f, -1.51f), Quaternion.identity, spell_parent.transform);
            add.transform.localPosition = new Vector3(2.21f, -2.198f, -1.51f);
            add.GetComponent<ChildButton>().parent = gameObject;
            add.GetComponent<ChildButton>().ButtonUse = "AddSpell";
        }
        
        channel_speed.text = Player.Instance.data.channel_chanting_speed[index].ToString();
    }

    public void DeleteChannel()
    {
        Debug.Log("DeleteChannel");
        Player.Instance.data.channel_chanting_speed.RemoveAt(channel_index);
        Player.Instance.data.spells_in_channels.RemoveAt(channel_index);
        SpellOrganise.Instance.Refresh();
    }

    public void IncreaseSpeed()
    {
        Debug.Log("Increase");
        if(SpellOrganise.Instance.steps_left >= 1)
        {
            Player.Instance.data.channel_chanting_speed[channel_index]++;
            SpellOrganise.Instance.Refresh();
        }

    }

    public void DecreaseSpeed()
    {
        Debug.Log("Decrease");
        if (Player.Instance.data.channel_chanting_speed[channel_index] >= 2)
        {
            Player.Instance.data.channel_chanting_speed[channel_index]--;
            SpellOrganise.Instance.Refresh();
        }
    }

    public void AddSpell()
    {
        GameObject temp = GameManager.Instance.SpawnWindowAtCamera(add_spell_window);
        temp.GetComponent<AddSpell>().index = channel_index;
        temp.GetComponent<AddSpell>().InitializeAddSpellWindow();
        //SpellOrganise.Instance.Refresh();
    }

    public void DeleteSpell(string spell_id)
    {
        PlayerSpell target = null;
        foreach(PlayerSpell spell in Player.Instance.data.spells_in_channels[channel_index].spell_list)
        {
            if(spell.spell_id == spell_id)
            {
                target = spell;
                //Player.Instance.data.spells_in_channels[channel_index].spell_list.Remove(spell);
            }
        }
        Player.Instance.data.spells_in_channels[channel_index].spell_list.Remove(target);
        SpellOrganise.Instance.Refresh();
    }
}
