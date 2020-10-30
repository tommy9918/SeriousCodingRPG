using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public string questID;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FadeControl>().StartFadeIn();
    }

    void OnTouchUp()
    {
        QuestManager.Instance.FinishQuest(questID);
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1);
    }
}
