using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBar : MonoBehaviour
{
    Motion motion;
    public int moveTime;
    public Text debugText;

    public PlayBar play_bar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (motion != null && motion.current <= moveTime)
        {
            float newScale = motion.UpdateValue();
            GetComponent<SpriteRenderer>().size = new Vector2(GetComponent<SpriteRenderer>().size.x, newScale);
        }
    }

    public void PopUp()
    {
        StartCoroutine(PopUpCoroutine());
    }

    IEnumerator PopUpCoroutine()
    {
        motion = new Motion(moveTime, 0, 4);
        GetComponent<SpriteRenderer>().size = new Vector2(GetComponent<SpriteRenderer>().size.x, 0);
        debugText.text = "";
        yield return new WaitForSeconds(0.5f);
        SetDebugText();
        debugText.gameObject.GetComponent<FadeControl>().StartFadeIn();
    }

    public void SetDebugText()
    {
        debugText.text = play_bar.GetDebugText();
    }
}
