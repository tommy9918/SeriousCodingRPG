using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairDragButton : MonoBehaviour
{
    Vector3 original_position;
    Vector3 block_original_position;
    Vector3 start_position;
    Vector3 end_position;
    bool dragging = true;
    public GameObject block_attached;
    public ScrollRect scroll;
    public SpellRepair repair_manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging && (Input.touchCount != 0 || Input.GetMouseButton(0)))
        {
            Vector2 finger = FingerPos();
            float offset = finger.y - start_position.y;
            transform.position = original_position + Vector3.up * offset;
            block_attached.transform.position = block_original_position + Vector3.up * offset;
        }

        else if ((Input.touchCount == 0 || !Input.GetMouseButton(0)) && dragging)
        {
            
            EndDragging();
            
            
        }

        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, block_attached.transform.localPosition.y - 0.38f, transform.localPosition.z);
        }
    }

    void OnTouchDown()
    {
        start_position = FingerPos();
        original_position = transform.position;
        block_original_position = block_attached.transform.position;
        StartDragging();
    }

    void StartDragging()
    {
        dragging = true;
        scroll.horizontal = false;
        scroll.vertical = false;
        repair_manager.scrolling_block = block_attached;
    }

    void EndDragging()
    {
        dragging = false;
        original_position = transform.position;
        block_original_position = block_attached.transform.position;
        scroll.horizontal = true;
        scroll.vertical = true;
        //repair_manager.scrolling_block = null;
        repair_manager.StopScrolling();
    }

    Vector2 FingerPos()
    {
        float distance = 0f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        return new Vector2(rayPoint.x, rayPoint.y);
    }
}
