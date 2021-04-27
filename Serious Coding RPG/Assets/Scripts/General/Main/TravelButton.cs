using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelButton : MonoBehaviour
{
    public string map_id;
    public string prerequisite;
    public GameObject requirement_text;
    public StageSelect controller;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.Instance.data.completedStage.Contains(prerequisite) || prerequisite == null || prerequisite == "")
        {
            requirement_text.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTravel()
    {
        if (Player.Instance.data.completedStage.Contains(prerequisite) || prerequisite == null || prerequisite == "")
        {
            GameManager.Instance.MapToMapChange(map_id);
            controller.CloseWindow();
        }
    }
}
