using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestConfirmPanel : MonoBehaviour
{
    
    public string questID;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ScaleChange>().StartAnimate();
    }

    void ClickedYes()
    {
        //Debug.Log("Yes");
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 1f);
        QuestManager.Instance.QuestUIStart(questID);
        //start quest with quest id
    }

    void ClickedNo()
    {
        //Debug.Log("No");
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 1f);
    }

    

    


}
