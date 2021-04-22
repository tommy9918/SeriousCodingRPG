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
    public Sprite wrong_bg;
    public Sprite right_bg;

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

    public void SetWrong()
    {
        GetComponent<SpriteRenderer>().sprite = wrong_bg;
    }

    public void SetRight()
    {
        GetComponent<SpriteRenderer>().sprite = right_bg;
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

    [ContextMenu("Collapse")]
    public void Collapse()
    {
        motion = new Motion(moveTime, GetComponent<SpriteRenderer>().size.y, 0);
        //GetComponent<SpriteRenderer>().size = new Vector2(GetComponent<SpriteRenderer>().size.x, 4);       
        debugText.gameObject.GetComponent<FadeControl>().StartFadeOut();
    }

    public void SetDebugText()
    {
        debugText.text = play_bar.GetDebugText();
    }
}
