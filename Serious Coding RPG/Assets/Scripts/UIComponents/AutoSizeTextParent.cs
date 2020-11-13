using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSizeTextParent : MonoBehaviour
{
    public Text theText;
    
    // Start is called before the first frame update
    void Start()
    {       
        theText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CalculateTextWidth());
        UpdateSize();
    }

    void UpdateSize()
    {
        int width = CalculateTextWidth() + 50;
        transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }

    int CalculateTextWidth()
    {
        int totalLength = 0;

        Font myFont = theText.font;  //chatText is my Text component
        CharacterInfo characterInfo = new CharacterInfo();

        string real_text = theText.text;
        if(transform.parent.gameObject.GetComponent<InputField>() != null)
        {
            real_text = transform.parent.gameObject.GetComponent<InputField>().text;
        }
        char[] arr = real_text.ToCharArray();

        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, theText.fontSize);

            totalLength += characterInfo.advance;
        }

        return totalLength;
    }
}
