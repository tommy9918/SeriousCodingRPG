using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIcon : MonoBehaviour
{
    public void Cancel()
    {
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 0.5f);
    }

    public void Delete()
    {
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 0.5f);
    }
}
