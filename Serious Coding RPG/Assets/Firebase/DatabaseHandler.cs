using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.Networking;

public class DatabaseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public  Text emailText;
    public  Text uidText;
    // public  InputField userNameText;
    public  InputField levelText;
    public  InputField scoreText;
    private FirebaseUser user;
    
    private string databaseURL = "https://spelloverflow-default-rtdb.firebaseio.com/"; 
    // private string AuthKey = "AIzaSyDh9qM2YHYAwd-F6uEwW6LmJj4p3yotduc";
    void Start()
    {
        if(AccountHandler.getUser() !=null)
            user =  AccountHandler.getUser();
        emailText.text += user.Email;
        uidText.text += user.UserId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onUpload()
    {
        if (AccountHandler.getUser() != null)
        {
            user =  AccountHandler.getUser();
            // Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            // {
            //     DisplayName = userNameText.text,
            // };
            //
            // user.UpdateUserProfileAsync(profile).ContinueWith(task => {
            //     if (task.IsCanceled) {
            //         Debug.LogError("UpdateUserProfileAsync was canceled.");
            //         return;
            //     }
            //     if (task.IsFaulted) {
            //         Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
            //         return;
            //     }
            //
            //     Debug.Log("User profile updated successfully.");
            // }); 
        }

        
        UserProfile userProfile = new UserProfile(user);
        userProfile.setScore(Int32.Parse(scoreText.text));
        userProfile.setLevel(Int32.Parse(levelText.text));
        RestClient.Put(databaseURL  + user.UserId + ".json" , userProfile);
        Debug.Log("Local userprofile uploaded");
    }

    [ContextMenu("onPushFile")]
    public void onPushFile()
    {
        string filename = "save.game";
        string filename2 = "playground.jpg";
        string filePath = "E:\\Downloads\\"+filename;
        string filePath2 = "E:\\Downloads\\"+filename2;
        
        if (File.Exists(filePath))
        {
            Debug.Log("local file found");
            // var fs = File.OpenRead(filePath);
            var url2file =
                "https://firebasestorage.googleapis.com/v0/b/spelloverflow.appspot.com/o/UserProfile%2F"+filename;
            // string headers = {"Content-Type": "image/png"};
            RestClient.Post(new RequestHelper
            {
                Uri = url2file,
                UploadHandler = new UploadHandlerFile(filePath),
                DefaultContentType = false
            } ).Then( response =>
            {
                Debug.Log("Rest Post succeeded");
            }).Catch(exception => Debug.Log(exception));
        } 
        
    }
    
    [ContextMenu("onDownloadFile")]
    public void onDownloadFile()
    {
        string filename = "save.game";
        // string filePath = "E:\\Downloads\\Firebase_storage\\"+filename;
        string filePath = "./Assets/Firebase/"+filename;
        var url2file =
            "https://firebasestorage.googleapis.com/v0/b/spelloverflow.appspot.com/o/UserProfile%2F"+filename+"?alt=media&token=";

        FileStream fs;
        if (!File.Exists(filePath))
        {
            // Create the file.
            fs = File.Create(filePath);
            Byte[] info = new System.Text.UTF8Encoding(true).GetBytes("This is some text in the file.");
            // Add some information to the file.
            fs.Write(info, 0, info.Length);
        }
        else
        {
            fs = File.OpenWrite(filePath);
        }

        RestClient.Get(new RequestHelper
        {
            Uri = url2file,
            DefaultContentType = false,
            ParseResponseBody = false
        } ).Then( response =>
        {
            Debug.Log("Rest Download succeeded");
            fs.Write(response.Data, 0, response.Data.Length);

        }).Catch(exception => Debug.Log(exception));
    
        
    }

}
