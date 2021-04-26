using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowManager : MonoBehaviour
{
    public Text PlayerName;
    public Image HP_bar;
    public Image MP_bar;
    public Text HP_text;
    public Text MP_text;
    public Text atk;
    public Text def;
    public Text speed;
    public Text memory;
    public Text multichant;

    [ContextMenu("UpdateStatInfo")]

    private void OnEnable()
    {
        //UpdateStatInfo();
        
    }
    public void UpdateStatInfo()
    {
        PlayerName.text = Player.Instance.data.name.ToUpper();
        HP_bar.fillAmount = Player.Instance.health / (float) Player.Instance.max_health;
        MP_bar.fillAmount = Player.Instance.mana / (float)Player.Instance.max_mana;
        HP_text.text = Player.Instance.health.ToString() + '/' + Player.Instance.max_health.ToString();
        MP_text.text = Player.Instance.mana.ToString() + '/' + Player.Instance.max_mana.ToString();
        atk.text = Player.Instance.ATK.ToString();
        def.text = Player.Instance.DEF.ToString();
        speed.text = Player.Instance.chanting_speed.ToString();
        memory.text = Player.Instance.max_equip_spells.ToString();
        multichant.text = "LV " + Player.Instance.spell_channels.ToString();

        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
        Destroy(gameObject, 0.6f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
