/*
 * This is a Module for handling the communication with
 * Firebase Server, including realtime database(for leaderboard)
 * and Storage(for game save file)
 */

using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DatabaseHandler : MonoBehaviour
{
    public  Text emailText;
    public  Text uidText;
    public  InputField levelText;
    public  InputField scoreText;
    private FirebaseUser user;
    private static FirebaseStorage storage;
    
    private const string databaseURL = "https://spelloverflow-default-rtdb.firebaseio.com/"; 
    void Start()
    {
        if (AccountHandler.getUser() != null)
        {
            user =  AccountHandler.getUser();
            Debug.Log("getUser() NOT NULL");
        }else Debug.Log("getUser() NULL");
        
        emailText.text += user.Email;
        uidText.text += user.UserId;
        storage = FirebaseStorage.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // For upload user records to leaderboard in
    // realtime database
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
    

    // For Debug only, uploading a file to storage
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
    
    //upload game save file to firebase storage
    public static void onPushSaveFile(string filePath)
    {

        if (File.Exists(filePath))
        {
            FirebaseUser user = null;
            string filename = "save.game";
            Debug.Log("local save file found, pushing to firebase");
            if (AccountHandler.getUser() != null)
            {
                user =  AccountHandler.getUser();
                var url2file =
                    "https://firebasestorage.googleapis.com/v0/b/spelloverflow.appspot.com/o/UserProfile%2F"+user.UserId+"%2F"+filename;
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
                Debug.Log("PushSaveFile failed: User not logged in yet");
            }

        }
        else
        {
            Debug.Log("PushSaveFile failed: File not found = "+ filePath);
        }

    }
    
    //For debug only, download file from firebase storage
    [ContextMenu("onDownloadFile")]
    public void onDownloadFile()
    {
        string filename = "save.game";
        // string filePath = "E:\\Downloads\\Firebase_storage\\"+filename;
        string filePath = Application.persistentDataPath + "/save.game";
        storage = FirebaseStorage.DefaultInstance;
        // var url2file =
        //     "https://firebasestorage.googleapis.com/v0/b/spelloverflow.appspot.com/o/UserProfile%2F"+filename+"?alt=media&token=";

        var uid = "CHzrvaJHL9cV4j5KxknpCPNFlg03";
        Debug.Log(storage);
        var pathReference =
            storage.GetReferenceFromUrl("gs://spelloverflow.appspot.com");
        Debug.Log(pathReference.Child("UserProfile").Path);
        StorageReference fileRef = pathReference.Child("UserProfile/CHzrvaJHL9cV4j5KxknpCPNFlg03/save.game");
        
        fileRef.GetDownloadUrlAsync().ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled) {
                Debug.Log("Download URL: " + task.Result);
                // ... now download the file via WWW or UnityWebRequest.
            }else Debug.Log("get URL failed");
        });

        fileRef.GetFileAsync(filePath).ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled) {
                
                Debug.Log("File downloaded.");
            }
            else
            {
                Debug.Log("download failed");
            }
        });

        // FileStream fs;
        // if (!File.Exists(filePath))
        // {
        //     // Create the file.
        //     fs = File.Create(filePath);
        //     Byte[] info = new System.Text.UTF8Encoding(true).GetBytes("This is some text in the file.");
        //     // Add some information to the file.
        //     fs.Write(info, 0, info.Length);
        // }
        // else
        // {
        //     fs = File.OpenWrite(filePath);
        // }
        //
        // RestClient.Get(new RequestHelper
        // {
        //     Uri = url2file,
        //     DefaultContentType = false,
        //     ParseResponseBody = false
        // } ).Then( response =>
        // {
        //     Debug.Log("Rest Download succeeded");
        //     fs.Write(response.Data, 0, response.Data.Length);
        //     Debug.LogFormat("Rest Download {0} succeeded", filename);
        //
        // }).Catch(exception => Debug.Log(exception));
        
    }
    
    
    public static void  onDownloadSaveFile()
    {
        string filename = "save.game";
        string filePath = Application.persistentDataPath + "/save.game";
        if (!File.Exists(filePath))
        {
            if (AccountHandler.getUser() != null)
            {
                storage = FirebaseStorage.DefaultInstance;
                var uid = AccountHandler.getUser().UserId;
                var childPath = "UserProfile/" +uid+"/save.game";
                var fileRef = storage.GetReference(childPath);
                
                // check if the file exists in firebase storage first
                 fileRef.GetDownloadUrlAsync().ContinueWithOnMainThread(task => {
                    if (!task.IsFaulted && !task.IsCanceled) {
                        Debug.Log("Download URL: " + task.Result);
                        // ... now download the file via WWW or UnityWebRequest.
                        fileRef.GetFileAsync(filePath).ContinueWithOnMainThread(task2 => {
                            if (!task2.IsFaulted && !task2.IsCanceled) {
                                Debug.Log("File downloaded.");
                                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                            }
                            else
                            {
                                Debug.Log("file download failed");
                            }
                        });
                    }
                    else
                    {
                        Debug.Log("get URL failed, file may not exist");
                        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                    }
                    
                });

                
            }
            else Debug.Log("DownloadSaveFile failed: User not logged in yet");
        }
        else Debug.Log("local profile exist, no need to download");
        

    }

}
