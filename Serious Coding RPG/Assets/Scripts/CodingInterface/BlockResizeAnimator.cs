using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockResizeAnimator : MonoBehaviour
{
    Motion motion;    

    // Update is called once per frame
    void Update()
    {
        if (motion != null && motion.current <= 45)
        {
            //float newScale = motion.UpdateValue();
            //transform.localScale = new Vector3(newScale, newScale, newScale);
            Vector2 new_size = motion.UpdatePositionValue();
            GetComponent<SpriteRenderer>().size = new_size;
        }
    }

    //[ContextMenu("Animate")]
    public void StartAnimate(Vector2 start_size, Vector2 final_size)
    {
        //Vector2 start_size1 = new Vector2(3.4f, 1.2f);
        //Vector2 final_size1 = new Vector2(5.4f, 7.2f);
        motion = new Motion(45, start_size, final_size);
        GetComponent<SpriteRenderer>().size = start_size;
    }
}
