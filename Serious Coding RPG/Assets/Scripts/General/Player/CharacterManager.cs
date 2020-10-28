using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float speed = 1;
    public Vector3 mousePos = new Vector3();
    public GameObject camera;
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
        Vector3 mousePos = camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
      
        float x_different = mousePos.x-transform.position.x;
        float y_different = mousePos.y-transform.position.y;
        if (Mathf.Abs(x_different) > Mathf.Abs(y_different))
        {
            if (x_different > 0)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                
            }
            else
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                
            }
        }
        else if (Mathf.Abs(x_different) < Mathf.Abs(y_different))
        {
            if (y_different > 0)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                
            }

            else
            {
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                
            }
        }
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);

    }

    void OnTouchUp()
    {
      
      
    }

    void OnTouchExit()
    {
      
    }
}
