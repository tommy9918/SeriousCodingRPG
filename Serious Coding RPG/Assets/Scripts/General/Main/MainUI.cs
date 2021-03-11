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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("UpdateMainUI")]
    public void UpdateMainUI()
    {
        PlayerName.text = Player.Instance.data.name.ToUpper();
        LevelNo.text = Player.Instance.data.level.ToString();
        ExpGem.fillAmount = 0.5f;
        HealthBar.fillAmount = 0.7f;
        ManaBar.fillAmount = 0.95f;
    }
}
