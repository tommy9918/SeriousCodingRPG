using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelButton : MonoBehaviour
{
    public string map_id;
    public string prerequisite;
    public GameObject requirement_text;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.Instance.data.completedStage.Contains(prerequisite))
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

    }
}
