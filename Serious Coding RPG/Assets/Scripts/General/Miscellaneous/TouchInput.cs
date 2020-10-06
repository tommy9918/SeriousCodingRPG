using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    private RaycastHit2D hit;
    private bool TouchEnded = true;

    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {

            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();




            hit = Physics2D.Raycast(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {

                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);


                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    TouchEnded = false;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    TouchEnded = true;
                }
                if (Input.GetMouseButton(0))
                {
                    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    TouchEnded = false;
                }


            }

            if (!TouchEnded)
            {
                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g) && g != null)
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                        TouchEnded = true;
                    }
                }
            }

        }



#endif

        if (Input.touchCount > 0)
        {

            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {

                hit = Physics2D.Raycast(GetComponent<Camera>().ScreenToWorldPoint(touch.position﻿), Vector2.zero);


                if (hit.collider != null)
                {

                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                        TouchEnded = false;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                        TouchEnded = true;
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                        TouchEnded = false;
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                        TouchEnded = true;
                    }

                }

            }

            if (!TouchEnded)
            {
                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g) && g != null)
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                        TouchEnded = true;
                    }
                }
            }
        }

    }
}
