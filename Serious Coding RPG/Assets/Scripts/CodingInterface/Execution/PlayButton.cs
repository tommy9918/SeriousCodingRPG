using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public CodingInterfaceManager coding_manager;
    void OnTouchUp()
    {
        //Debug.Log("Hello");
        coding_manager.RunCode();
        Deactivate();
        //transform.parent.gameObject.GetComponent<PlayBar>().SwitchToDebug();
    }

    public void Deactivate()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<ScaleChange>().StartAnimate();
    }

    public void Reactivate()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(ReactivateRoutine());
    }

    IEnumerator ReactivateRoutine()
    {
        GetComponent<ScaleChange>().StartAnimateReverse();
        yield return new WaitForSeconds(0.4f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
