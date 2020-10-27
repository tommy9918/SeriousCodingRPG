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
      // Debug.Log("Down");
    }

    void OnTouchStay()
    {
      // Debug.Log("Stay");
      // Debug.Log(transform.position);
      mousePos = camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
      // Debug.Log(mousePos);
      float x_different = mousePos.x-transform.position.x;
      float y_different = mousePos.y-transform.position.y;
      if (Mathf.Abs(x_different)>Mathf.Abs(y_different)){
        if (x_different>0)
          transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        else {
          transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
      }
      else if (Mathf.Abs(x_different)<Mathf.Abs(y_different)) {
        if(y_different>0){
          transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }else {
          transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
      }

    }

    void OnTouchUp()
    {
      // Debug.Log("Up");
    }

    void OnTouchExit()
    {
      // Debug.Log("Exit");
    }
}
