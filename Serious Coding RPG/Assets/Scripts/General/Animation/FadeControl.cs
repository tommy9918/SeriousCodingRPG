using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    //doesnt work if there is other script controlling alpha

    public SpriteRenderer[] sprites;
    public Text[] texts;
    public Image[] images;
    public Color[] colors;
    public Color[] colors2;
    public Color[] colors3;
    public bool FadeIn = false;
    public bool FadeOut = false;
    public bool FadeInFromStart = false;
    public bool FadeOutFromStart = false;
    public float speed;
    public float current = 1;

    public bool unscaledTime;

    public bool debug;

    // Use this for initialization
    void Awake()
    {

        sprites = GetComponentsInChildren<SpriteRenderer>();
        texts = GetComponentsInChildren<Text>();
        images = GetComponentsInChildren<Image>();
        colors = new Color[sprites.Length];
        colors2 = new Color[texts.Length];
        colors3 = new Color[images.Length];

        getColors();

        //FadeIn = true;
        if (FadeIn)
        {
            for (int i = 0; i <= sprites.Length - 1; i++)
            {
                Color tmp = colors[i];
                tmp.a = current;
                sprites[i].GetComponent<SpriteRenderer>().color = tmp;
                //gameObject.GetComponent<SpriteRenderer>().color = tmp;

            }

            for (int i = 0; i <= texts.Length - 1; i++)
            {
                Color tmp = colors2[i];
                tmp.a = current;
                texts[i].GetComponent<Text>().color = tmp;
                //gameObject.GetComponent<Text>().color = tmp;

            }

            for (int i = 0; i <= images.Length - 1; i++)
            {
                Color tmp = colors3[i];
                tmp.a = current;
                images[i].GetComponent<Image>().color = tmp;
                //gameObject.GetComponent<Image>().color = tmp;

            }
        }

    }

    public void GetAllComponenets()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        texts = GetComponentsInChildren<Text>();
        images = GetComponentsInChildren<Image>();
        colors = new Color[sprites.Length];
        colors2 = new Color[texts.Length];
        colors3 = new Color[images.Length];

        getColors();
    }

    public bool isChild(GameObject obj)
    {
        return obj.transform.IsChildOf(gameObject.transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale > 0 || unscaledTime)
        {
            if (FadeIn)
            {
                for (int i = 0; i <= sprites.Length - 1; i++)
                {
                    Color tmp = colors[i];
                    tmp.a = current * colors[i].a;
                    sprites[i].GetComponent<SpriteRenderer>().color = tmp;
                    //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                }

                for (int i = 0; i <= texts.Length - 1; i++)
                {
                    Color tmp = colors2[i];
                    tmp.a = current * colors2[i].a;
                    texts[i].GetComponent<Text>().color = tmp;
                    //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                }

                for (int i = 0; i <= images.Length - 1; i++)
                {
                    Color tmp = colors3[i];
                    tmp.a = current * colors3[i].a;
                    images[i].GetComponent<Image>().color = tmp;
                    //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                }


                current += 1 / speed;
                if (current >= 1)
                {
                    FadeIn = false;
                    current = 1;

                    for (int i = 0; i <= sprites.Length - 1; i++)
                    {
                        Color tmp = colors[i];
                        tmp.a = current * colors[i].a;
                        sprites[i].GetComponent<SpriteRenderer>().color = tmp;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                    }

                    for (int i = 0; i <= texts.Length - 1; i++)
                    {
                        Color tmp = colors2[i];
                        tmp.a = current * colors2[i].a;
                        texts[i].GetComponent<Text>().color = tmp;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                    }

                    for (int i = 0; i <= images.Length - 1; i++)
                    {
                        Color tmp = colors3[i];
                        tmp.a = current * colors3[i].a;
                        images[i].GetComponent<Image>().color = tmp;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;

                    }
                }
            }

            else if (FadeOut)
            {
                for (int i = 0; i <= sprites.Length - 1; i++)
                {
                    if (isChild(sprites[i].gameObject))
                    {
                        Color tmp = colors[i];
                        tmp.a = current * colors[i].a;
                        sprites[i].GetComponent<SpriteRenderer>().color = tmp;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;
                    }

                }

                for (int i = 0; i <= texts.Length - 1; i++)
                {
                    if (isChild(texts[i].gameObject))
                    {
                        Color tmp = colors2[i];
                        tmp.a = current * colors2[i].a;
                        texts[i].GetComponent<Text>().color = tmp;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;
                    }

                }

                for (int i = 0; i <= images.Length - 1; i++)
                {
                    if (isChild(images[i].gameObject))
                    {
                        Color tmp = colors3[i];
                        tmp.a = current * colors3[i].a;
                        images[i].GetComponent<Image>().color = tmp;
                        //gameObject.GetComponent<SpriteRenderer>().color = tmp;
                    }

                }


                current -= 1 / speed;
                if (current <= 0)
                {
                    FadeOut = false;
                    current = 0;

                    for (int i = 0; i <= sprites.Length - 1; i++)
                    {
                        if (isChild(sprites[i].gameObject))
                        {
                            Color tmp = colors[i];
                            tmp.a = current * colors[i].a;
                            sprites[i].GetComponent<SpriteRenderer>().color = tmp;
                            //gameObject.GetComponent<SpriteRenderer>().color = tmp;
                        }

                    }

                    for (int i = 0; i <= texts.Length - 1; i++)
                    {
                        if (isChild(texts[i].gameObject))
                        {
                            Color tmp = colors2[i];
                            tmp.a = current * colors2[i].a;
                            texts[i].GetComponent<Text>().color = tmp;
                            //gameObject.GetComponent<SpriteRenderer>().color = tmp;
                        }

                    }

                    for (int i = 0; i <= images.Length - 1; i++)
                    {
                        if (isChild(images[i].gameObject))
                        {
                            Color tmp = colors3[i];
                            tmp.a = current * colors3[i].a;
                            images[i].GetComponent<Image>().color = tmp;
                            //gameObject.GetComponent<SpriteRenderer>().color = tmp;
                        }

                    }
                }
            }
        }

        if (debug) Debug.Log(current);
    }

    private void OnEnable()
    {
        //Debug.Log("Enabled!");
        if (FadeInFromStart)
        {
            FadeIn = true;
            current = 0;
            for (int i = 0; i <= sprites.Length - 1; i++)
            {
                Color tmp = colors[i];
                //Debug.Log(tmp);
                tmp.a = 0;
                sprites[i].GetComponent<SpriteRenderer>().color = tmp;

            }

            for (int i = 0; i <= texts.Length - 1; i++)
            {
                Color tmp = colors2[i];
                //Debug.Log(tmp);
                tmp.a = 0;
                texts[i].GetComponent<Text>().color = tmp;

            }

            for (int i = 0; i <= images.Length - 1; i++)
            {
                Color tmp = colors3[i];
                //Debug.Log(tmp);
                tmp.a = 0;
                images[i].GetComponent<Image>().color = tmp;

            }
        }
        if (FadeOutFromStart)
        {
            FadeOut = true;
            current = 0;
            for (int i = 0; i <= sprites.Length - 1; i++)
            {
                Color tmp = colors[i];
                //Debug.Log(tmp);
                tmp.a = 1;
                sprites[i].GetComponent<SpriteRenderer>().color = tmp;

            }

            for (int i = 0; i <= texts.Length - 1; i++)
            {
                Color tmp = colors2[i];
                //Debug.Log(tmp);
                tmp.a = 1;
                texts[i].GetComponent<Text>().color = tmp;

            }

            for (int i = 0; i <= images.Length - 1; i++)
            {
                Color tmp = colors3[i];
                //Debug.Log(tmp);
                tmp.a = 1;
                images[i].GetComponent<Image>().color = tmp;

            }
        }
    }

    public void getColors()
    {
        for (int i = 0; i <= colors.Length - 1; i++)
        {
            colors[i] = sprites[i].GetComponent<SpriteRenderer>().color;
            if (colors[i].a == 0)
            {
                colors[i].a = 1f;
            }

        }

        for (int i = 0; i <= colors2.Length - 1; i++)
        {
            colors2[i] = texts[i].GetComponent<Text>().color;
            if (colors2[i].a == 0)
            {
                colors2[i].a = 1f;
            }

        }

        for (int i = 0; i <= colors3.Length - 1; i++)
        {
            colors3[i] = images[i].GetComponent<Image>().color;
            if (colors3[i].a == 0)
            {
                colors3[i].a = 1f;
            }

        }
    }

    public void StartFadeIn()
    {
        GetAllComponenets();
        getColors();
        FadeIn = true;
        FadeOut = false;
        current = 0;       
    }

    public void StartFadeOut()
    {
        GetAllComponenets();
        getColors();
        FadeIn = false;
        FadeOut = true;
        current = 1;
    }
}
