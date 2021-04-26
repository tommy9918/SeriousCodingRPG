using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public string stage_id;
    public StageSelect select_panel;
    public GameObject clear_label;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.CheckFinishStage(stage_id))
        {
            clear_label.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        GameManager.Instance.StartBattle(stage_id);
        select_panel.CloseWindow();
    }
}
