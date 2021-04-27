using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using UnityEngine.SceneManagement;



public class AccountHandler : MonoBehaviour
{
    
    public InputField emailText;
    // public InputField usernameText;
    public InputField passwordText;
    public InputField verifyText;
    public InputField usernameText;
    private Firebase.Auth.FirebaseAuth auth;
    private static Firebase.Auth.FirebaseUser user;
    
    // Start is called before the first frame update
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                // displayName = user.DisplayName ?? "";
                // emailAddress = user.Email ?? "";
                // photoUrl = user.PhotoUrl ?? "";
            }
        }
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
    


    public void onSignIn()
    {
         auth.SignInWithEmailAndPasswordAsync(emailText.text, passwordText.text).ContinueWithOnMainThread( task => {
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
                string uid = user.UserId;
                Debug.LogFormat( "user information : name:{0} email:{1} uid:{2}", name, email, uid );
            }
            DatabaseHandler.onDownloadSaveFile();
            // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        });
        

    }

    public void onGoogleSignIn()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration {
            RequestIdToken = true,
            // Copy this value from the google-service.json file.
            // oauth_client with type == 3
            // WebClientId = "320930175122-0dbmppjll7ln7vfd8f88b82gerrigmgo.apps.googleusercontent.com" //From Google API Console
            WebClientId = "320930175122-jk18h4huuh1f76rfmc1qddfk81v2ocs8.apps.googleusercontent.com" //From firebase
        };

        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn ();

        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser> ();
        signIn.ContinueWithOnMainThread (task => {
            if (task.IsCanceled) {
                signInCompleted.SetCanceled ();
                Debug.Log("GoogleLogIn: task canceled");
            } else if (task.IsFaulted) {
                signInCompleted.SetException (task.Exception);
                Debug.Log("GoogleLogIn: task faulted, " + task.Exception);
            } else {
                Debug.Log("GoogleLogIn: task completed");

                Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential (((Task<GoogleSignInUser>)task).Result.IdToken, null);
                auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread ( authTask => {
                    if (authTask.IsCanceled) {
                        signInCompleted.SetCanceled();
                        Debug.Log("GoogleLogIn: auth task canceled");
                    } else if (authTask.IsFaulted) {
                        signInCompleted.SetException(authTask.Exception);
                        Debug.Log("GoogleLogIn: auth task faulted");
                    } else {
                        signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);
                        user = ((Task<FirebaseUser>) authTask).Result;
                        Debug.Log("GoogleLogIn: auth task completed");
                        DatabaseHandler.onDownloadSaveFile();
                        // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                    }
                    
                });
            }
        });
    }

    // for verifying the password in sign up
    public void onVerifierChange()
    {
        // Debug.Log("value changed");
        verifyText.image.color = verifyText.text != passwordText.text ? Color.red : Color.white;
        
    }


    [ContextMenu("sign out")]
    public void onSignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene("SignInScene", LoadSceneMode.Single);
    }

    public static FirebaseUser getUser()
    {
        return user;
    }

    public void createNewAccount()
    {
        SceneManager.LoadScene("SignUpScene", LoadSceneMode.Single);
    }

    public void onGoBack()
    {
        SceneManager.LoadScene("SignInScene", LoadSceneMode.Single);
    }
}
