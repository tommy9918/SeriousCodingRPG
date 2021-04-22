using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubBlockManager : MonoBehaviour
{
    public float width;
    public float height;
    public Text skill_name_text;

    public enum BlockType
    {
        VARIABLE,
        VALUE,
        //VALUE_OPT,
        CONDITIONAL,
    }
    public BlockType type;
    public string value_type;
    public List<GameObject> block_sites;
    public GameObject value_reference;

    public GameObject delete_button;

    public bool horizontal;
    public bool vertical;

    public string block_type;
    public bool can_delete;


    // Start is called before the first frame update
    void Start()
    {
        InititiateBlockSize();
    }

    public void InititiateBlockSize()
    {
        if (horizontal) SetSubBlockPositionHorizontal();
        else if (vertical) SetSubBlockPositionVertical();

        //width = GetComponent<SpriteRenderer>().size.x;
        //height = GetComponent<SpriteRenderer>().size.y;


    }

    [ContextMenu("GetOriginalSize")]
    public void GetOriginalSize()
    {
        if (horizontal) SetSubBlockPositionHorizontal();
        else if (vertical) SetSubBlockPositionVertical();

        foreach (BlockSiteManager bsm in GetComponentsInChildren<BlockSiteManager>())
        {
            bsm.width = bsm.gameObject.GetComponent<SpriteRenderer>().size.x;
            bsm.height = bsm.gameObject.GetComponent<SpriteRenderer>().size.y;
        }
        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;

    }

    public void StartDeleteBlock()
    {
        if (GetComponent<LongPressDrag>().accepted)
        {
            GameObject temp = Instantiate(delete_button);
            temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
            temp.GetComponent<DeleteIcon>().block = gameObject;
            temp.GetComponent<ScaleChange>().StartAnimate();
        }
    }

    public void DeleteBlock()
    {
        //Debug.Log("deleteblock!");
        GetComponent<ScaleChange>().StartAnimateReverse();
        if (transform.parent.GetComponent<BlockSiteManager>() != null && transform.parent.GetComponent<BlockSiteManager>().horizontal)
        {
            transform.parent.GetComponent<BlockSiteManager>().inserted_block = null;
            transform.parent.GetComponent<BlockSiteManager>().DeleteBlock();
            Destroy(gameObject, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AutoUpdateSize();
        if (GetComponent<SkillBlockInit>() != null)
        {
            SetSkillBlockPosition();
        }
        else
        {
            if (horizontal) SetSubBlockPositionHorizontal();
            else if (vertical) SetSubBlockPositionVertical();
        }
    }

    public void SetSkillBlockPosition()
    {
        float max_x_size = -1f;
        float max_y_size = -1f;
        float total_x_offset = CalculateTextWidth() / 100f + 0.65f;
        for (int i = 1; i <= block_sites.Count - 1; i++)
        {
            total_x_offset += 0.1f;

            if (block_sites[i].transform.parent == gameObject.transform)
            {
                //Debug.Log(total_x_offset);
                block_sites[i].transform.localPosition = new Vector3(total_x_offset, -0.245f, block_sites[i].transform.localPosition.z);
            }
            else
            {
                //block_sites[i].transform.parent.localPosition = new Vector3(total_x_offset, -0.1f, block_sites[i].transform.localPosition.z);
            }

            if (block_sites[i].GetComponent<SpriteRenderer>() != null)
            {
                //Debug.Log(block_sites[i].GetComponent<SpriteRenderer>().size.x);
                total_x_offset += block_sites[i].GetComponent<SpriteRenderer>().size.x;
                if (block_sites[i].GetComponent<SpriteRenderer>().size.x > max_x_size)
                {
                    max_x_size = block_sites[i].GetComponent<SpriteRenderer>().size.x;
                }
                if (block_sites[i].GetComponent<SpriteRenderer>().size.y > max_y_size)
                {
                    max_y_size = block_sites[i].GetComponent<SpriteRenderer>().size.y;
                }
            }          
        }
        GetComponent<SpriteRenderer>().size = new Vector2(total_x_offset + 0.3f, max_y_size + 0.5f);
    }

    int CalculateTextWidth()
    {
        Text theText = skill_name_text;
        int totalLength = 0;
        //Text theText = GetComponentInChildren<Text>();
        Font myFont = theText.font;  //chatText is my Text component
        CharacterInfo characterInfo = new CharacterInfo();

        string real_text = theText.text;
        char[] arr = real_text.ToCharArray();

        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, theText.fontSize);

            totalLength += characterInfo.advance;
        }

        return totalLength;
    }

    public void AutoUpdateSize()
    {
        //width = GetComponent<SpriteRenderer>().size.x;
        //height = GetComponent<SpriteRenderer>().size.y;
    }

    public void SetSubBlockPositionHorizontal()
    {
        if (block_sites != null)
        {
            float max_x_size = -1f;
            float max_y_size = -1f;
            float total_x_offset = 0;
            for (int i = 0; i <= block_sites.Count - 1; i++)
            {
                total_x_offset += 0.1f;
                
                if (block_sites[i].transform.parent == gameObject.transform)
                {
                    //Debug.Log(total_x_offset);
                    block_sites[i].transform.localPosition = new Vector3(total_x_offset, -0.1f, block_sites[i].transform.localPosition.z);
                }
                else
                {
                    //block_sites[i].transform.parent.localPosition = new Vector3(total_x_offset, -0.1f, block_sites[i].transform.localPosition.z);
                }

                if (block_sites[i].GetComponent<SpriteRenderer>() != null)
                {
                    //Debug.Log(block_sites[i].GetComponent<SpriteRenderer>().size.x);
                    total_x_offset += block_sites[i].GetComponent<SpriteRenderer>().size.x;
                    if (block_sites[i].GetComponent<SpriteRenderer>().size.x > max_x_size)
                    {
                        max_x_size = block_sites[i].GetComponent<SpriteRenderer>().size.x;
                    }
                    if (block_sites[i].GetComponent<SpriteRenderer>().size.y > max_y_size)
                    {
                        max_y_size = block_sites[i].GetComponent<SpriteRenderer>().size.y;
                    }
                }
                else
                {
                    float offset = 100f;
                    total_x_offset += block_sites[i].GetComponent<RectTransform>().sizeDelta.x / offset - 0.5f;
                    if (block_sites[i].GetComponent<RectTransform>().sizeDelta.x / offset > max_x_size)
                    {
                        max_x_size = block_sites[i].GetComponent<RectTransform>().sizeDelta.x / offset;
                    }
                    if (block_sites[i].GetComponent<RectTransform>().sizeDelta.y / offset > max_y_size)
                    {
                        max_y_size = block_sites[i].GetComponent<RectTransform>().sizeDelta.y / offset;
                    }
                }

            }
            GetComponent<SpriteRenderer>().size = new Vector2(total_x_offset + 0.1f, max_y_size + 0.2f);
        }
    }

    public void SetSubBlockPositionVertical()
    {
        if (block_sites != null)
        {
            float max_x_size = -1f;
            float max_y_size = -1f;
            float total_y_offset = 0;
            for (int i = 0; i <= block_sites.Count - 1; i++)
            {
                total_y_offset += 0.1f;
                block_sites[i].transform.localPosition = new Vector3(0.1f, total_y_offset, block_sites[i].transform.localPosition.z);
                total_y_offset += block_sites[i].GetComponent<SpriteRenderer>().size.y;
                if (block_sites[i].GetComponent<SpriteRenderer>().size.x > max_x_size)
                {
                    max_x_size = block_sites[i].GetComponent<SpriteRenderer>().size.x;
                }
                if (block_sites[i].GetComponent<SpriteRenderer>().size.y > max_y_size)
                {
                    max_y_size = block_sites[i].GetComponent<SpriteRenderer>().size.y;
                }

            }
            GetComponent<SpriteRenderer>().size = new Vector2(max_x_size + 0.2f, total_y_offset + 0.1f);
        }
    }

    public BlockSiteManager.SiteType getCorrectSiteType(GameObject this_block_site)
    {
        GameObject other_site;
        if (GameObject.ReferenceEquals(this_block_site, block_sites[0]))
        {
            other_site = block_sites[2];
        }
        else other_site = block_sites[0];
        
        if(other_site.GetComponent<BlockSiteManager>().inserted_block == null)
        {
            return BlockSiteManager.SiteType.CONDITONAL_VALUE;
        }
        else if(other_site.GetComponent<BlockSiteManager>().inserted_block.GetComponent<SubBlockManager>().type == BlockType.CONDITIONAL)
        {
            return BlockSiteManager.SiteType.CONDITIONAL;
        }
        else return BlockSiteManager.SiteType.VARIABLE_VALUE;
    }

    public string getCorrectValueType(GameObject this_block_site)
    {
        GameObject other_site;
        if (GameObject.ReferenceEquals(this_block_site, block_sites[0]))
        {
            other_site = block_sites[2];
        }
        else other_site = block_sites[0];

        if (other_site.GetComponent<BlockSiteManager>().inserted_block == null)
        {
            return "all";
        }
        else return other_site.GetComponent<BlockSiteManager>().inserted_block.GetComponent<SubBlockManager>().value_type;
    }
}
