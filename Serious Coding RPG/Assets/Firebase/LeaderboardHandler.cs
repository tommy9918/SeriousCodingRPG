using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
// using System.Security.Policy;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using Proyecto26;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = System.Random;

public class LeaderboardHandler : MonoBehaviour
{
    private Random random = new Random();
    public Text leaderboardText;
    private string databaseURL = "https://spelloverflow-default-rtdb.firebaseio.com/";
    private string top10 = "";

    private FirebaseDatabase fdb;

    // Start is called before the first frame update
    void Start()
    {
        fdb = FirebaseDatabase.DefaultInstance;
        //GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();
        GetComponent<Dim>().StartDim();

    }

    // Update is called once per frame
    void Update()
    {
        leaderboardText.text = top10;
    }

    public void CloseWindow()
    {
        //GetComponent<FadeControl>().StartFadeOut();
        GetComponent<ScaleChange>().StartAnimateReverse();
        GetComponent<Dim>().RemoveDim();
        Destroy(gameObject, 0.5f);
    }

    public string GetRandomString(int n)
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path.Substring(0, n); // Return 8 character string
    }

    [ContextMenu("TestUpload")]
    public void onTestUpload()
    {
        UserProfile userProfile = new UserProfile();
        userProfile.setScore(random.Next(999));
        userProfile.setLevel(random.Next(10));
        var randomStr = GetRandomString(8);
        userProfile.uid = randomStr;
        userProfile.name = "Tommy_" + randomStr;
        userProfile.email = randomStr + "@dummy.com";
        RestClient.Put(databaseURL + "/leaderboard/" + randomStr + ".json", userProfile);
        Debug.Log("Local user score uploaded");
    }

    // [ContextMenu("TestLeaderboard")]
    public void onTestLeaderboard()
    {
        var rank = 10;
        // Uri dburl = new Uri("https://spelloverflow-default-rtdb.firebaseio.com/leaderboard");
        fdb.GetReference("leaderboard")
            .OrderByChild("level")
            .LimitToLast(10)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Fail To retrieve data");
                }
                else if (task.IsCompleted)
                {
                    // Debug.Log("retrieve task completed");
                    if (task.Result == null)
                    {
                        Debug.Log("task result null!");
                        return;
                    }
                    else if (!task.Result.HasChildren)
                    {
                        Debug.Log("task result no children!");
                        return;
                    }

                    top10 = "";
                    rank = Convert.ToInt32(task.Result.ChildrenCount);
                    DataSnapshot snapshot = task.Result;
                    // var foo = snapshot.Child("mvhz9vl9").Child("score").Value.ToString();
                    // Debug.Log(foo);
                    foreach (DataSnapshot h in snapshot.Children)
                    {

                        var buffer = rank.ToString() + " ";
                        buffer += h.Child("name").Value.ToString() + "\t" + h.Child("level").Value.ToString();
                        Debug.Log(buffer);
                        top10 = buffer + "\n" + top10;
                        rank--;
                    }

                    Debug.Log(top10);

                }
            });
    }

    public void showScoreRank()
    {
        var rank = 10;
        // Uri dburl = new Uri("https://spelloverflow-default-rtdb.firebaseio.com/leaderboard");
        fdb.GetReference("leaderboard")
            .OrderByChild("score")
            .LimitToLast(10)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Fail To retrieve data");
                }
                else if (task.IsCompleted)
                {
                    // Debug.Log("retrieve task completed");
                    if (task.Result == null)
                    {
                        Debug.Log("task result null!");
                        return;
                    }
                    else if (!task.Result.HasChildren)
                    {
                        Debug.Log("task result no children!");
                        return;
                    }

                    top10 = "";
                    rank = Convert.ToInt32(task.Result.ChildrenCount);
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot h in snapshot.Children)
                    {

                        var buffer = rank.ToString() + " ";
                        buffer += h.Child("name").Value.ToString() + "\t" + h.Child("score").Value.ToString();
                        Debug.Log(buffer);
                        top10 = buffer + "\n" + top10;
                        rank--;
                    }

                    Debug.Log(top10);

                }
            });
    }

}
