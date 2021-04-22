using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject map_ui;
    public GameObject coding_ui;
    public GameObject battle_ui;
    public GameObject main_ui;

    public GameObject black_transition;

    void Awake()
    {
        if (Instance == null)   //singleton Player instance, easy for referencing in other scripts
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

    public IEnumerator StartMission(string quest_id)
    {
        Vector3 pos = Player.Instance.gameObject.transform.position;
        black_transition.transform.position = new Vector3(pos.x, pos.y, black_transition.transform.position.z);       
        black_transition.GetComponent<FadeControl>().StartFadeIn();
        main_ui.GetComponent<MainUI>().HideMainUI();
        yield return new WaitForSeconds(0.5f);
        map_ui.SetActive(false);
        coding_ui.GetComponent<CodingInterfaceManager>().questID = quest_id;
        coding_ui.transform.position = (Vector2)Player.Instance.gameObject.transform.position;
        coding_ui.GetComponent<CodingInterfaceManager>().InitializeCodingUI();
        coding_ui.SetActive(true);
        black_transition.GetComponent<FadeControl>().StartFadeOut();

    }

    public IEnumerator FinishMission(string quest_id)
    {
        main_ui.GetComponent<MainUI>().ShowMainUI();
        black_transition.GetComponent<FadeControl>().StartFadeIn();
        
        yield return new WaitForSeconds(0.5f);
        map_ui.SetActive(true);
        coding_ui.GetComponent<CodingInterfaceManager>().ResetCodingUI();
        coding_ui.SetActive(false);
        black_transition.GetComponent<FadeControl>().StartFadeOut();
        QuestManager.Instance.FinishQuest(quest_id);
    }
}
