using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockReconstructor : MonoBehaviour
{
    public GameObject code_block_parent;
    public GameObject[] all_blocks_reference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int TypeToIndex(string type)
    {
        switch (type)
        {
            case "num":
                return 0;
            case "char":
                return 1;
            case "variable":
                return 2;
            case "assign":
                return 3;
            case "if":
                return 4;
            case "input":
                return 5;
            case "jump":
                return 6;
            case "output":
                return 7;
            case "and":
                return 8;
            case "or":
                return 9;
            case "equal":
                return 10;
            case "larger":
                return 11;
            case "larger_equal":
                return 12;
            case "smaller":
                return 13;
            case "smaller_equal":
                return 14;
            case "plus":
                return 15;
            case "minus":
                return 16;
            case "multiply":
                return 17;
            case "divide":
                return 18;
            case "remainder":
                return 19;
        }
        return -1;
    }
}
