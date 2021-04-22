using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleShoot : MonoBehaviour
{
    public Vector3 destination;
    public Vector3 current_direction;
    public float offset;
    public float speed;
    public bool started;

    public GameObject start_effect;
    public GameObject end_effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Vector2.Distance(transform.position, destination) >= 0.2f)
            {
                transform.position += current_direction * speed;
                current_direction = destination - transform.position;
                //current_direction = current_direction.normalized;
                offset *= 0.95f;
                //speed *= 0.97f;

                current_direction = Quaternion.Euler(0, 0, offset) * current_direction;
            }
            else
            {
                started = false;
                //Destroy(this);
                if (end_effect != null)
                {
                    Instantiate(end_effect, transform.position, Quaternion.identity);
                }
                Destroy(gameObject, 0.5f);
            }
        }
    }

    public void Shoot(Vector3 destination)
    {
        started = true;
        this.destination = destination;
        Vector3 dist = destination - transform.position;
        current_direction = dist;
        offset = Random.Range(-dist.magnitude*2f, dist.magnitude*2f);
        offset = 12;
        current_direction = Quaternion.Euler(0, 0, offset) * current_direction;

        if(start_effect != null)
        {
            Instantiate(start_effect, transform.position, Quaternion.identity);
        }
    }

    [ContextMenu("TestShoot")]
    public void TestShoot()
    {
        Shoot(new Vector3(0, -8, 0));
    }
}
