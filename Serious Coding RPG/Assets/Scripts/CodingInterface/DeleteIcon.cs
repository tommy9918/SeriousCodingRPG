using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIcon : MonoBehaviour
{
    public GameObject block;

    public void Cancel()
    {
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 0.5f);
    }

    public void Delete()
    {
        if(block.GetComponent<BlockManager>() != null)
        {
            block.GetComponent<BlockManager>().DeleteBlock();
        }
        else if(block.GetComponent<SubBlockManager>() != null)
        {
            block.GetComponent<SubBlockManager>().DeleteBlock();
        }
        GetComponent<ScaleChange>().StartAnimateReverse();
        Destroy(gameObject, 0.5f);
    }
}
