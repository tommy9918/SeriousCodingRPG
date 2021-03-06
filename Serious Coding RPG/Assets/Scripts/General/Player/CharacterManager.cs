﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float speed = 1;
    public Vector3 mousePos = new Vector3();
    public GameObject cam;
    public float slow_down;

    public GameObject walkable;
    public GameObject obstacle;
    public GameObject exit;
    public List<Collider2D> walkable_collider;
    public List<Collider2D> obstacle_collider;
    public List<Collider2D> exit_colliders;

    void Start()
    {
        
    }

    public void InitializeCharacter()
    {
        GetAllCollider();
        cam = Player.Instance.gameObject;
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1.13f, cam.transform.position.z);
        Player.Instance.character = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTouchDown()
    {
      
    }

    public void GetAllCollider()
    {
        walkable_collider = new List<Collider2D>();
        obstacle_collider = new List<Collider2D>();
        exit_colliders = new List<Collider2D>();
        foreach(Collider2D c in walkable.GetComponents<Collider2D>())
        {
            walkable_collider.Add(c);
        }
        foreach (Collider2D c in obstacle.GetComponents<Collider2D>())
        {
            obstacle_collider.Add(c);
        }
        foreach(Collider2D c in exit.GetComponents<Collider2D>())
        {
            exit_colliders.Add(c);
        }
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
                //transform.position += new Vector3(speed * Time.deltaTime * slow_factor, 0, 0);
                Vector3 new_pos = transform.position + new Vector3(speed * Time.deltaTime * slow_factor, 0, 0);
                if (CanWalk(new_pos)) transform.position = new_pos;
            }
            else
            {
                //transform.position += new Vector3(-speed * Time.deltaTime * slow_factor, 0, 0);
                Vector3 new_pos = transform.position + new Vector3(-speed * Time.deltaTime * slow_factor, 0, 0);
                if (CanWalk(new_pos)) transform.position = new_pos;
            }
        }
        else if (Mathf.Abs(x_different) < Mathf.Abs(y_different))
        {
            float slow_factor = Mathf.Min(Mathf.Abs(y_different) / slow_down, 1f);
            if (y_different > 0)
            {
                //transform.position += new Vector3(0, speed * Time.deltaTime * slow_factor, 0);
                Vector3 new_pos = transform.position + new Vector3(0, speed * Time.deltaTime * slow_factor, 0);
                if (CanWalk(new_pos)) transform.position = new_pos;
            }

            else
            {
                //transform.position += new Vector3(0, -speed * Time.deltaTime * slow_factor, 0);
                Vector3 new_pos = transform.position + new Vector3(0, -speed * Time.deltaTime * slow_factor, 0);
                if (CanWalk(new_pos)) transform.position = new_pos;
            }
        }
        cam.transform.position = new Vector3(transform.position.x, transform.position.y+1.13f, cam.transform.position.z);

    }

    bool CanWalk(Vector2 pos)
    {
        bool pass = false;
        foreach(Collider2D c in walkable_collider)
        {
            if (c.bounds.Contains(pos))
            {
                pass = true;
                break;
            }
        }
        if (!pass) return false;
        foreach(Collider2D c in obstacle_collider)
        {
            if (c.bounds.Contains(pos))
            {
                return false;
            }
        }

        if (!CanExit(pos))
        {
            return true;
        }
        else
        {
            GameManager.Instance.SelectStage();
            return false;
        }
    }

    bool CanExit(Vector2 pos)
    {
        foreach(Collider2D c in exit_colliders)
        {
            if (c.bounds.Contains(pos))
            {
                return true;
            }
        }
        return false;
    }

    void OnTouchUp()
    {
      
      
    }

    void OnTouchExit()
    {
      
    }
}
