using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackObject : MonoBehaviour
{
    public GameObject poof;
    public GameObject target;
    public List<ParticleSystem> particles;
    public int current;
    public int damage_carried;

    // Start is called before the first frame update
    void Start()
    {
        particles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
    }

    // Update is called once per frame
    void Update()
    {
        if(current < 50)
        {
            current++;
        }
        else if(current == 50)
        {
            GameObject temp = Instantiate(poof, target.transform);
            temp.transform.localPosition = new Vector3(0, 0, -0.1f);
            foreach(ParticleSystem ps in particles)
            {
                ps.enableEmission = false;
            }
            target.GetComponent<MonsterManager>().damage(damage_carried);
            Destroy(gameObject, 1);
            Destroy(temp, 1);
            current++;
        }
    }

    public void InitializeFAO()
    {
        Vector3 pos = transform.localPosition;
        GetComponent<MoveTo>().startPosition = pos;
        GetComponent<MoveTo>().destination = target.transform.localPosition;
        GetComponent<MoveTo>().moveTime = 50;
        GetComponent<MoveTo>().ReplayMotion();
    }
}
