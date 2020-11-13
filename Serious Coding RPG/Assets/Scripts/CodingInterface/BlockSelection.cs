using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelection : MonoBehaviour
{
    public GameObject[] all_blocks_reference;
    public List<GameObject> summoned_blocks;

    public GameObject scroll_list;

    private void OnEnable()
    {
        summoned_blocks = new List<GameObject>();
        SummonAllBlockAsChild();
        GetComponent<FadeControl>().GetAllComponenets();
        StartAnimate();
        
    }

    private void OnDisable()
    {
        DeleteAllBLocks();
    }

    void DeleteAllBLocks()
    {
        foreach(GameObject blk in summoned_blocks)
        {
            if (blk.transform.parent == scroll_list.transform)
            {
                Destroy(blk);
            }
        }
        summoned_blocks.Clear();
    }

    void SummonAllBlockAsChild()
    {
        float total_y_offset = 0f;
        for(int i = 0; i <= all_blocks_reference.Length - 1; i++)
        {
            total_y_offset -= 0.3f;
            GameObject temp = Instantiate(all_blocks_reference[i], scroll_list.transform);
            temp.transform.localPosition = new Vector3(-3.17f, total_y_offset, -0.01f);

            if(temp.GetComponent<BlockManager>() != null)
            {
                temp.GetComponent<BlockManager>().InititiateBlockSize();
            }
            else if(temp.GetComponent<SubBlockManager>() != null)
            {
                temp.GetComponent<SubBlockManager>().InititiateBlockSize();
            }
            temp.GetComponent<LongPressDrag>().coding_manager = transform.parent.gameObject;

            total_y_offset -= temp.GetComponent<SpriteRenderer>().size.y;

            summoned_blocks.Add(temp);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //StartAnimate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimate()
    {
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
    }

    
}
