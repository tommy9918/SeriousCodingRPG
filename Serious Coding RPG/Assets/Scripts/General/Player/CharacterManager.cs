using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float speed = 1;
    public Vector3 mousePos = new Vector3();
    public GameObject cam;
    public float slow_down;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTouchDown()
    {
      
    }

    void OnTouchStay()
    {    
        Vector3 mousePos = cam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
      
        float x_different = mousePos.x-transform.position.x;
        float y_different = mousePos.y-transform.position.y;
        if (Mathf.Abs(x_different) > Mathf.Abs(y_different))
        {
            float slow_factor = Mathf.Min(Mathf.Abs(x_different) / slow_down, 1f);
            if (x_different > 0)
            {
                transform.position += new Vector3(speed * Time.deltaTime * slow_factor, 0, 0);
                
            }
            else
            {
                transform.position += new Vector3(-speed * Time.deltaTime * slow_factor, 0, 0);
                
            }
        }
        else if (Mathf.Abs(x_different) < Mathf.Abs(y_different))
        {
            float slow_factor = Mathf.Min(Mathf.Abs(y_different) / slow_down, 1f);
            if (y_different > 0)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime * slow_factor, 0);
                
            }

            else
            {
                transform.position += new Vector3(0, -speed * Time.deltaTime * slow_factor, 0);
                
            }
        }
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

    }

    void OnTouchUp()
    {
      
      
    }

    void OnTouchExit()
    {
      
    }
}
