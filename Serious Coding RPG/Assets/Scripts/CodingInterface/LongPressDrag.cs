﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongPressDrag : MonoBehaviour
{
    Vector2 finger_start_position;
    Vector2 finger_current_position;
    Vector2 offset;
    float original_z;
    bool pressed;
    bool dragging;
    int confirm_duration = 10;
    int current = 0;
    public bool accepted;
    public bool repairing;
    public GameObject coding_manager;

    // Start is called before the first frame update
    void Start()
    {
        original_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            current++;
            finger_current_position = FingerPos();
            if(Vector2.Distance(finger_current_position, finger_start_position) >= 0.05)
            {
                pressed = false;
                current = 0;
            }
            if(current >= confirm_duration)
            {
                pressed = false;
                current = 0;
                if (!accepted)
                {
                    startDragging();
                }
            }
        }

        if (dragging && (Input.touchCount != 0 || Input.GetMouseButton(0)))
        {
            Vector2 finger = FingerPos();
            finger = finger - offset;
            transform.position = new Vector3(finger.x, finger.y, original_z - 0.1f);
        }
        
        else if((Input.touchCount == 0 || !Input.GetMouseButton(0)) && dragging)
        {
            //Debug.Log("dump");
            endDragging();
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (!accepted)
            {
                GetComponent<SetMaskInteration>().SetInteraction("none");
                GetComponent<ScaleChange>().StartAnimateReverse();
                Destroy(gameObject, 0.5f);
            }
        }
    }

    void OnTouchDown()
    {
        finger_start_position = FingerPos();
        pressed = true;
    }

    void OnTouchUp()
    {
        current = 0;
        pressed = false;
    }

    

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }

    void startDragging()
    {
        
        dragging = true;
        GetComponent<BoxCollider2D>().enabled = false;
        finger_start_position = FingerPos();
        offset = finger_start_position - (Vector2)transform.position;
        coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block = gameObject;
        coding_manager.GetComponent<CodingInterfaceManager>().DisableScrolling();

        BlockSiteManager[] sites = gameObject.GetComponentsInChildren<BlockSiteManager>();
        foreach(BlockSiteManager st in sites)
        {
            st.coding_manager = coding_manager;
        }

        transform.parent = null;

        if(coding_manager.GetComponent<CodingInterfaceManager>().block_selection.active == true)
        {
            coding_manager.GetComponent<CodingInterfaceManager>().CloseBlockSelection();
        }

        GetComponent<SetMaskInteration>().SetInteraction("none");
        GetComponent<SetMaskInteration>().SetMask("Default", 0);
    }

    void endDragging()
    {
        GetComponent<SetMaskInteration>().SetInteraction("inside");
        dragging = false;
        pressed = false;
        current = 0;
        GetComponent<BoxCollider2D>().enabled = true;
        coding_manager.GetComponent<CodingInterfaceManager>().active_dragging_block = null;
        coding_manager.GetComponent<CodingInterfaceManager>().EnableScrolling();
        this.enabled = false;

        foreach (InputField ipf in GetComponentsInChildren<InputField>())
        {
            ipf.interactable = true;
        }
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = true;
        }
    }
}
