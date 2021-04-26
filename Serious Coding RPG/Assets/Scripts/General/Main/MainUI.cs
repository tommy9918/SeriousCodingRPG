using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public Text PlayerName;
    public Text LevelNo;
    public Image ExpGem;
    public Image HealthBar;
    public Image ManaBar;

    public GameObject top;
    public GameObject bottom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMainUI();
    }

    [ContextMenu("UpdateMainUI")]
    public void UpdateMainUI()
    {
        PlayerName.text = Player.Instance.data.name.ToUpper();
        LevelNo.text = Player.Instance.data.level.ToString();
        ExpGem.fillAmount = Player.Instance.data.EXP/Player.Instance.max_exp;
        HealthBar.fillAmount = Player.Instance.health/Player.Instance.max_health;
        ManaBar.fillAmount = Player.Instance.mana / Player.Instance.max_mana;
    }

    [ContextMenu("HideMainUI")]
    public void HideMainUI()
    {
        top.GetComponent<MoveTo>().ReplayMotion();
        bottom.GetComponent<MoveTo>().ReplayMotion();
    }

    [ContextMenu("ShowMainUI")]
    public void ShowMainUI()
    {
        top.GetComponent<MoveTo>().ReverseMotion();
        bottom.GetComponent<MoveTo>().ReverseMotion();
    }
}
