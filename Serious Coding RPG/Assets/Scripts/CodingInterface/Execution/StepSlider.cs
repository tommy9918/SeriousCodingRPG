using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSlider : MonoBehaviour
{
    public float left_limit;
    public float right_limit;
    bool dragging;
    public float slide_value;

    // Update is called once per frame
    void Update()
    {
        if (dragging && (Input.touchCount != 0 || Input.GetMouseButton(0)))
        {
            Vector2 finger = FingerPos();
            finger.x = Mathf.Clamp(finger.x, left_limit, right_limit);
            transform.position = new Vector3(finger.x, transform.position.y, transform.position.z);
            slide_value = (finger.x - left_limit) / (right_limit - left_limit);
            transform.parent.gameObject.GetComponent<PlayBar>().UpdateStep(slide_value);
        }

        else if ((Input.touchCount == 0 || !Input.GetMouseButton(0)) && dragging)
        {
            dragging = false;
        }
    }

    void OnTouchDown()
    {
        dragging = true;
    }

    void OnTouchUp()
    {
        dragging = false;
    }

    public void SetToStep0()
    {
        transform.position = new Vector3(left_limit, transform.position.y, transform.position.z);
        slide_value = 0;
    }

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }
}
