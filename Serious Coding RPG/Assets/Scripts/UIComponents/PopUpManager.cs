using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public GameObject dim_reference;
    public List<GameObject> dim_instance;
    public List<GameObject> pop_up_instance;
    public Collider2D confirm_layer;

    public static PopUpManager Instance;

    public GameObject unlock_block;

    int base_layer = 10;
    int current_max_layer = 10;
    float current_max_z = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        dim_instance = new List<GameObject>();
        pop_up_instance = new List<GameObject>();
    }

    void SetLayer(GameObject obj)
    {
        
        foreach(SpriteRenderer sr in obj.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = current_max_layer;
        }
        foreach(ParticleSystemRenderer ps in obj.GetComponentsInChildren<ParticleSystemRenderer>())
        {
            ps.sortingOrder = current_max_layer;
        }
        foreach(Canvas c in obj.GetComponentsInChildren<Canvas>())
        {
            c.sortingOrder = current_max_layer;
        }
        current_max_layer++;
        
    }

    [ContextMenu("OpenPopUpTest")]
    public void OpenPopUpTest()
    {
        OpenPopUp(Instantiate(unlock_block));
    }

    [ContextMenu("ClosePopUp")]
    public void ClosePopUp()
    {
        StartCoroutine(ClosePopUpRoutine(pop_up_instance.Count - 1));
    }

    

    IEnumerator ClosePopUpRoutine(int index)
    {
        //Debug.Log(index);
        dim_instance[index].GetComponent<FadeControl>().StartFadeOut();
        if (pop_up_instance[index].GetComponent<FadeControl>() != null)
        {
            pop_up_instance[index].GetComponent<FadeControl>().StartFadeOut();
        }
        yield return new WaitForSeconds(0.6f);
        Destroy(dim_instance[index]);
        Destroy(pop_up_instance[index]);
        dim_instance.RemoveAt(index);
        pop_up_instance.RemoveAt(index);
        current_max_layer -= 2;
        current_max_z += 1f;

        if(dim_instance.Count == 0)
        {
            confirm_layer.enabled = false;
        }
    }

    public void OpenPopUp(GameObject pop_up)
    {
        dim_instance.Add(Instantiate(dim_reference, (Vector2)Player.Instance.gameObject.transform.position, Quaternion.identity));
        dim_instance[dim_instance.Count - 1].GetComponent<FadeControl>().StartFadeIn();
        SetLayer(dim_instance[dim_instance.Count - 1]);

        pop_up_instance.Add(pop_up);
        SetLayer(pop_up_instance[pop_up_instance.Count - 1]);
        Vector3 pos = Player.Instance.gameObject.transform.position;
        confirm_layer.transform.position = new Vector3(pos.x, pos.y, confirm_layer.transform.position.z);
        confirm_layer.enabled = true;
    }

    public void OpenComplexPopUp(GameObject pop_up)
    {
        dim_instance.Add(Instantiate(dim_reference, (Vector2)Player.Instance.gameObject.transform.position, Quaternion.identity));
        dim_instance[dim_instance.Count - 1].GetComponent<FadeControl>().StartFadeIn();
        SetLayer(dim_instance[dim_instance.Count - 1]);

        pop_up_instance.Add(pop_up);
        SetLayer(pop_up_instance[pop_up_instance.Count - 1]);

        //confirm_layer.enabled = true;
    }

    public void RemoveDim()
    {
        StartCoroutine(RemoveDimRoutine(pop_up_instance.Count - 1));
    }

    IEnumerator RemoveDimRoutine(int index)
    {
        dim_instance[index].GetComponent<FadeControl>().StartFadeOut();
        yield return new WaitForSeconds(0.6f);
        Destroy(dim_instance[index]);
        dim_instance.RemoveAt(index);
        pop_up_instance.RemoveAt(index);
    }

    public void UnlockBlockPopUp()
    {
        OpenPopUp(Instantiate(unlock_block, (Vector2)Player.Instance.gameObject.transform.position, Quaternion.identity));
    }
}
