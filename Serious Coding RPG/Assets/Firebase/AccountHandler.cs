using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;



public class AccountHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField emailText;
    // public InputField usernameText;
    public InputField passwordText;
    public InputField usernameText;
    private Firebase.Auth.FirebaseAuth auth;
    private static Firebase.Auth.FirebaseUser user;
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void onSignUp()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailText.text, passwordText.text).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = usernameText.text,
            };

            newUser.UpdateUserProfileAsync(profile);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.Email, newUser.UserId);
            SceneManager.LoadScene("SignInScene", LoadSceneMode.Single);
        });
        
        
    }
    
    
    // public void DoThePublicThing()
    // {
    //     Debug.Log("DoThePublicThing");
    // }

    public  void onSignIn()
    {
         auth.SignInWithEmailAndPasswordAsync(emailText.text, passwordText.text).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            
            user = auth.CurrentUser;
            if (user != null) {
                string name = user.DisplayName;
                string email = user.Email;
                System.Uri photo_url = user.PhotoUrl;
                // The user's Id, unique to the Firebase project.
                // Do NOT use this value to authenticate with your backend server, if you
                // have one; use User.TokenAsync() instead.
                string uid = user.UserId;
                Debug.LogFormat( "user information : name:{0} email:{1} uid:{2}", name, email, uid );
            }
            SceneManager.LoadScene("UploadUserProfile", LoadSceneMode.Single);
        });
        
        // SceneManager.LoadScene("UploadUserProfile", LoadSceneMode.Single);

    }

    // Update is called once per frame
    [ContextMenu("sign out")]
    public void onSignOut()
    {
        auth.SignOut();
    }

    public static FirebaseUser getUser()
    {
        return  user;
    }

    public void createNewAccount()
    {
        SceneManager.LoadScene("SignUpScene", LoadSceneMode.Single);

    }
}
