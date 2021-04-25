using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellOrganise : MonoBehaviour
{
    public static SpellOrganise Instance;
    public GameObject skill_ref;

    public List<GameObject> learned_skills;
    public GameObject learned_parent;

    public Text settings_title;
    public Text settings_value;

    public GameObject channel_parent;
    public GameObject channel_ref;
    public List<GameObject> channel_list;

    public GameObject add_button_ref;

    public int steps_left;
    public int channel_left;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeOrganisePanel();
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

    public void Refresh()
    {
        
        foreach (GameObject obj in learned_skills)
        {
            Destroy(obj);
        }
        foreach(GameObject obj in channel_list)
        {
            Destroy(obj);
        }
        InitializeOrganisePanel();
    }

    [ContextMenu("InitializeOrganisePanel")]
    public void InitializeOrganisePanel()
    {
        
        learned_skills = new List<GameObject>();

       
        foreach (BattleSpell spell in GameManager.Instance.GetLearntSpell())
        {
            GameObject temp = Instantiate(skill_ref, learned_parent.transform);
            temp.GetComponent<SpellManager>().spell = spell;
            temp.GetComponent<SpellManager>().DisableNamePart();
            temp.GetComponent<SpellManager>().InitializeSpell();
            temp.GetComponent<SpellManager>().can_expand_information = true;
            temp.GetComponent<SetMaskInteration>().SetInteraction("inside");
            learned_skills.Add(temp);
        }

        for (int i = 0; i <= learned_skills.Count - 1; i++)
        {
            learned_skills[i].transform.localPosition = new Vector3(1.11f + (i % 4) * 1.9f, -1.14f - (i / 4) * -2f, -1f);
        }

        settings_title.text = "<b><i>" + MasterTextManager.Instance.LoadText("STEPSLEFT") + ": \n" + MasterTextManager.Instance.LoadText("UNUSECHANNEL") + ": </i></b>";
        settings_value.text = ValueText();

        for(int i = 0; i <= Player.Instance.data.channel_chanting_speed.Count - 1; i++)
        {
            GameObject temp = Instantiate(channel_ref, channel_parent.transform);
            temp.transform.localPosition = new Vector3(3.047f, -1.4f - 2.38f*i, 0.9f);
            temp.GetComponent<ChannelManager>().InitializeChannel(i);
            channel_list.Add(temp);
            if(channel_left >= 1 && steps_left >= 1 && GameManager.Instance.GetUnequippedSpell().Count >= 1)
            {
                GameObject add = Instantiate(add_button_ref, channel_parent.transform);
                add.transform.localPosition = new Vector3(3.73f, -3.37f - 2.38f * i, 0.9f);
                add.transform.localScale = new Vector3(1, 1, 1);
                add.GetComponent<ChildButton>().parent = gameObject;
                add.GetComponent<ChildButton>().ButtonUse = "AddChannel";
                channel_list.Add(add);
            }
        }

        if(Player.Instance.data.channel_chanting_speed.Count == 0 && GameManager.Instance.GetUnequippedSpell().Count >= 1)
        {
            GameObject add = Instantiate(add_button_ref, channel_parent.transform);
            add.transform.localPosition = new Vector3(3.73f, -3.37f - 2.38f * -1f, 0.9f);
            add.transform.localScale = new Vector3(1, 1, 1);
            add.GetComponent<ChildButton>().parent = gameObject;
            add.GetComponent<ChildButton>().ButtonUse = "AddChannel";
            channel_list.Add(add);
        }
    }

    string ValueText()
    {
        string final_text = "<b><i>";
        int max_step = Player.Instance.chanting_speed;
        int unused_step = max_step;
        
        foreach(int x in Player.Instance.data.channel_chanting_speed)
        {
            unused_step -= x;
        }
        steps_left = unused_step;
        if (max_step == unused_step)
        {
            final_text += "<color=#0AFF00>" + unused_step + "</color>/" + "<color=#0AFF00>" + max_step + "</color>\n";
        }
        else
        {
            final_text += unused_step + "/" + "<color=#0AFF00>" + max_step + "</color>\n";
        }
        int max_channel = Player.Instance.spell_channels;
        int unused_channel = max_channel - Player.Instance.data.spells_in_channels.Count;
        channel_left = unused_channel;
        if (max_channel == unused_channel)
        {
            final_text += "<color=#0AFF00>" + unused_channel + "</color>/" + "<color=#0AFF00>" + max_channel + "</color>\n";
        }
        else
        {
            final_text += unused_channel + "/" + "<color=#0AFF00>" + max_channel + "</color>";
        }


        return final_text + "</i></b>";
    }

    public void AddChannel()
    {
        Debug.Log("Add channel!");
        Player.Instance.data.channel_chanting_speed.Add(1);
        Player.Instance.data.spells_in_channels.Add(new PlayerData.PlayerSpellList());
        Refresh();
    }
}
