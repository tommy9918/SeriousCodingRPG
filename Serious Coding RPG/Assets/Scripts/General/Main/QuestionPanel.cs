using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionPanel : MonoBehaviour
{
    public Text description;
    public string question_id;
    public List<string> answers;
    public List<int> shuffled_array;
    public Text question;
    public List<Text> answer_button_labels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeQuestionPanel(string question_id)
    {
        string question_text = "";
        MasterTextManager.Instance.LoadQuestionByID(question_id, out question_text, out answers);
        question.text = question_text;
        ShuffleAnswers();
        for(int i = 0; i <= 3; i++)
        {
            answer_button_labels[i].text = answers[i];
        }
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();
    }

    void ShuffleAnswers()
    {
        shuffled_array = new List<int>();
        for(int i = 1; i <= 4; i++)
        {
            shuffled_array.Add(i);
        }
        for(int i = 0; i <= 3; i++)
        {
            int target = Random.Range(i, 4);
            int temp1 = shuffled_array[i];
            string temp2 = answers[i];
            shuffled_array[i] = shuffled_array[target];
            answers[i] = answers[target];
            shuffled_array[target] = temp1;
            answers[target] = temp2;
        }
    }

    public void CloseWindow()
    {
        GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
    }

    public void AnswerCorrect()
    {
        BattleManager.Instance.progress_bar.Progress();
        BattleManager.Instance.ToNextStage();
        CloseWindow();
    }

    public void AnswerWrong()
    {
        BattleManager.Instance.progress_bar.DeProgress();
        BattleManager.Instance.ToNextStage();
        CloseWindow();
    }

    public void Answer1()
    {
        if(shuffled_array[0] == 1)
        {
            AnswerCorrect();
        }
        else
        {
            AnswerWrong();
        }
    }

    public void Answer2()
    {
        if (shuffled_array[1] == 1)
        {
            AnswerCorrect();
        }
        else
        {
            AnswerWrong();
        }
    }

    public void Answer3()
    {
        if (shuffled_array[2] == 1)
        {
            AnswerCorrect();
        }
        else
        {
            AnswerWrong();
        }
    }

    public void Answer4()
    {
        if (shuffled_array[3] == 1)
        {
            AnswerCorrect();
        }
        else
        {
            AnswerWrong();
        }
    }
}
