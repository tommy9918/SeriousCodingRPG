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
            temp.GetComponent<SpellManager>().spell = GameManager.Instance.GetSpellBySpellID(Player.Instance.data.spells_in_channels[index].spell_list[i].spell_id);
            temp.GetComponent<SpellManager>().InitializeSpell();
            temp.GetComponent<SpellManager>().DisableNamePart();
            if(i== Player.Instance.data.spells_in_channels[index].spell_list.Count - 1)
            {
                Instantiate(add_icon, new Vector3(2.21f + 3.78f * (i+1), -2.198f, -1.51f), Quaternion.identity, spell_parent.transform);
            }

        }
        channel_speed.text = Player.Instance.data.channel_chanting_speed[index].ToString();
    }

    public void DeleteChannel()
    {
        SpellOrganise.Instance.Refresh();
    }

    public void IncreaseSpeed()
    {

    }

    public void DecreaseSpeed()
    {

    }

    public void AddSpell()
    {

    }
}
