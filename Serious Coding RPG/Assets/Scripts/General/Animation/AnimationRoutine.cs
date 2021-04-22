using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRoutine : MonoBehaviour
{

    public List<string> animations;
    public List<int> durations;
    public bool DoNotAutoStart;

    // Start is called before the first frame update
    void Start()
    {
        if (!DoNotAutoStart)
        {
            StartCoroutine(AnimateCoroutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation()
    {
        StartCoroutine(AnimateCoroutine());
    }

    IEnumerator AnimateCoroutine()
    {
        for(int i = 0; i <= animations.Count - 1; i++)
        {
            string[] animation_list = animations[i].Split(',');
            foreach (string a in animation_list)
            {
                switch (a)
                {
                    case "fadein":
                        GetComponent<FadeControl>().speed = durations[i];
                        GetComponent<FadeControl>().StartFadeIn();
                        break;
                    case "fadeout":
                        GetComponent<FadeControl>().speed = durations[i];
                        GetComponent<FadeControl>().StartFadeOut();
                        break;
                    case "scale":
                        GetComponent<ScaleChange>().moveTime = durations[i];
                        GetComponent<ScaleChange>().StartAnimate();
                        break;
                    case "scalereverse":
                        GetComponent<ScaleChange>().moveTime = durations[i];
                        GetComponent<ScaleChange>().StartAnimateReverse();
                        break;
                }
            }
            yield return new WaitForSeconds(durations[i] / 60f);
        }
    }


}
