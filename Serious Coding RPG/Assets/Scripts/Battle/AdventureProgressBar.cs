using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureProgressBar : MonoBehaviour
{
    public GameObject wizard_icon;
    public GameObject dot_reference;
    public int total_stage;
    public List<float> x_coors;
    public int current;
    public Vector2 x_boundary = new Vector2(-3.118f, 2.25f);

    // Start is called before the first frame update
    void Start()
    {
        //InitializeCoordinatesList(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCoordinatesList(int stage)
    {
        total_stage = stage;
        x_coors = new List<float>();
        x_coors.Add(x_boundary.x);
        for(int i = 0; i <= stage - 3; i++)
        {
            float length = x_boundary.y - x_boundary.x;
            float interval = length / (stage - 1);
            x_coors.Add(x_boundary.x + interval * (i + 1));
        }
        x_coors.Add(x_boundary.y);
        current = 0;

        for (int i = 0; i <= stage - 1; i++)
        {
            GameObject temp = Instantiate(dot_reference, transform);
            temp.transform.localPosition = new Vector3(x_coors[i], 0f, -0.1f);
        }
    }

    [ContextMenu("Progress")]
    public void Progress()
    {
        if (current != total_stage - 1)
        {
            Vector3 pos = wizard_icon.transform.localPosition;
            wizard_icon.GetComponent<MoveTo>().startPosition = pos;
            wizard_icon.GetComponent<MoveTo>().destination = new Vector3(x_coors[++current], pos.y, pos.z);
            wizard_icon.GetComponent<MoveTo>().ReplayMotion();
        }
        GetComponent<ColorFlash>().Flash(new Color(0, 1, 0, 1));
    }

    [ContextMenu("DeProgress")]
    public void DeProgress()
    {
        if (current != 0)
        {
            Vector3 pos = wizard_icon.transform.localPosition;
            wizard_icon.GetComponent<MoveTo>().startPosition = pos;
            wizard_icon.GetComponent<MoveTo>().destination = new Vector3(x_coors[--current], pos.y, pos.z);
            wizard_icon.GetComponent<MoveTo>().ReplayMotion();
        }
        GetComponent<ColorFlash>().Flash(new Color(1, 0, 0, 1));
    }
}
