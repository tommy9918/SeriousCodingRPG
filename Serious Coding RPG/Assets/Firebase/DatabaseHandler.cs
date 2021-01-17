using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class DatabaseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public  Text emailText;
    public  Text uidText;
    public  InputField userNameText;
    public  InputField levelText;
    public  InputField scoreText;
    private FirebaseUser user;
    
    private string databaseURL = "https://spelloverflow-default-rtdb.firebaseio.com/"; 
    private string AuthKey = "AIzaSyDh9qM2YHYAwd-F6uEwW6LmJj4p3yotduc";
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
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = userNameText.text,
            };

            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
            }); 
        }
        emailText.text += user.Email;
        uidText.text += user.UserId;
        
        UserProfile userProfile = new UserProfile(user);
        userProfile.setScore(Int32.Parse(scoreText.text));
        userProfile.setLevel(Int32.Parse(levelText.text));
        RestClient.Put(databaseURL  + user.UserId + ".json" , userProfile);
        Debug.Log("Local userprofile uploaded");
    }

}
