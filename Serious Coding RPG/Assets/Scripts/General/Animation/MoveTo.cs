using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public bool affectedByTime = false;
    public bool movefromstart;

    public float moveTime;
    public Vector2 startPosition;
    public Vector2 destination;
    Motion motion;
    public bool moving;

    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        if (movefromstart)
        {
            motion = new Motion(moveTime, startPosition, destination);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (motion != null && motion.current <= moveTime && moving)
        {
            //transform.position = motion.UpdatePositionValue();
            Vector2 temp = motion.UpdatePositionValue();
            transform.localPosition = new Vector3(temp.x, temp.y, transform.localPosition.z);
        }
        else
        {
            moving = false;
        }
    }

    public void InitializeNewMotion(string direction, float offset, float moveTime)
    {
        transform.position = destination;
        if (string.Equals(direction, "up"))
        {
            float y = transform.position.y + offset;
            startPosition = new Vector2(transform.position.x, y);
        }
        else if (string.Equals(direction, "down"))
        {
            float y = transform.position.y - offset;
            startPosition = new Vector2(transform.position.x, y);
        }
        else if (string.Equals(direction, "left"))
        {
            float x = transform.position.x - offset;
            startPosition = new Vector2(x, transform.position.y);
        }
        else if (string.Equals(direction, "right"))
        {
            float x = transform.position.x + offset;
            startPosition = new Vector2(x, transform.position.y);
        }

        motion = new Motion(moveTime, startPosition, new Vector2(transform.position.x, transform.position.y));
        destination = transform.position;
        transform.position = startPosition;
    }

    public void ReverseMotion()
    {
        /*transform.position = destination;
        Vector2 finalPosition;

        if (up)
        {
            float y = transform.position.y + offset;
            startPosition = new Vector2(transform.position.x, y);

        }
        else if (down)
        {
            float y = transform.position.y - offset;
            startPosition = new Vector2(transform.position.x, y);

        }
        else if (left)
        {
            float x = transform.position.x - offset;
            startPosition = new Vector2(x, transform.position.y);

        }
        else if (right)
        {
            float x = transform.position.x + offset;
            startPosition = new Vector2(x, transform.position.y);

        }

        motion = new Motion(moveTime, transform.position, startPosition);
        destination = startPosition;*/
        //transform.position = startPosition;
        motion = new Motion(moveTime, transform.localPosition, startPosition);
        moving = true;
        //transform.position = new Vector3(destination.x, destination.y, transform.position.z);
        //transform.position = destination;
    }

    public void ReplayMotion()
    {

        motion = new Motion(moveTime, transform.localPosition, destination);
        moving = true;

    }

    public void ReplayStartEndMotion()
    {
        motion = new Motion(moveTime, startPosition, destination);
        moving = true;
    }
}
