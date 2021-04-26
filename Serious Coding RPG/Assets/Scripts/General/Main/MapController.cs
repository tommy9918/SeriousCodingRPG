using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance;
    public GameObject entire_scene;
    public List<GameObject> village_reference;
    public GameObject character;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceScene(string sceneid)
    {
        if (entire_scene.GetComponent<VillageController>().VillageID == sceneid) return;
        GameObject new_scene = null;
        foreach(GameObject village in village_reference)
        {
            if(village.GetComponent<VillageController>().VillageID == sceneid)
            {
                new_scene = village;
                break;
            }
        }
        Destroy(entire_scene);
        entire_scene = Instantiate(new_scene);
        entire_scene.GetComponent<VillageController>().InitializeVillage();
    }
}
