using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using Proyecto26.Common;
using UnityEngine.Networking;
using Random = System.Random;

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
        if (AccountHandler.getUser() != null)
        {
            user =  AccountHandler.getUser();
            Debug.Log("getUser() NOT NULL");
        }else Debug.Log("getUser() NULL");
        
        
        
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
        }
        UserProfile userProfile = new UserProfile(user);
        userProfile.setScore(Int32.Parse(scoreText.text));
        userProfile.setLevel(Int32.Parse(levelText.text));
        RestClient.Put(databaseURL +"/leaderboard/" + user.UserId + ".json" , userProfile);
        Debug.Log("Local user score uploaded");
    }
    

    [ContextMenu("onPushFile")]
    public void onPushFile()
    {
        string filename = "save.game";
        // string filename2 = "playground.jpg";
        string filePath = Application.persistentDataPath+"/" + filename;
        // string filePath2 = "E:\\Downloads\\"+filename2;
        
        if (File.Exists(filePath))
        {
            Debug.Log("local file found");
            // var fs = File.OpenRead(filePath);
            if (AccountHandler.getUser() != null)
            {
                user =  AccountHandler.getUser();
            }
            var url2file =
                "https://firebasestorage.googleapis.com/v0/b/spelloverflow.appspot.com/o/UserProfile%2F"+user.UserId+"%2F"+filename;
            // string headers = {"Content-Type": "image/png"};
            RestClient.Post(new RequestHelper
            {
                Uri = url2file,
                UploadHandler = new UploadHandlerFile(filePath),
                DefaultContentType = false
            } ).Then( response =>
            {
                Debug.LogFormat("Rest Post {0} succeeded",filename);
            }).Catch(exception => Debug.Log(exception));
        }
        else
        {
            Debug.Log("Potato: File not found = "+ filePath);
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
            Debug.LogFormat("Rest Download {0} succeeded", filename);

        }).Catch(exception => Debug.Log(exception));
    
        
    }

}
