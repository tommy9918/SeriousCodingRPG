using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerData data;
    public GameObject character;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)   //singleton Player instance, easy for referencing in other scripts
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        data = LoadData();     //load player data
    }   

    public bool checkQuestStatus(int id)    //0 = not finished, 1 = finished
    {
        foreach(int questid in data.completedTask)
        {
            if(id == questid)
            {
                return true;
            }
        }
        return false;
    }

    public PlayerData LoadData()
    {
        return SaveLoad.Load();
    }

    public void SaveData()
    {

        SaveLoad.Save(this);

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveLoad.Save(this);           
        }
        
    }

    void OnApplicationQuit()
    {
        SaveLoad.Save(this);        
    }
}
