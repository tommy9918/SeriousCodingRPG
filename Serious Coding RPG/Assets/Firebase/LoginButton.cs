
using System.Collections;
using System.Collections.Generic;
using Firebase;
// using Firebase.Extensions;
// using System.Threading.Tasks;
// using Google;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
// using Proyecto26;
// using System.Threading.Tasks;

public class LoginButton : MonoBehaviour 
{
  
  
  // public static string googleIdToken;
  // public static string googleAccessToken;
  // private Firebase.Auth.FirebaseAuth auth;
  // private Firebase.Auth.FirebaseUser user;
  //
  // private string displayName;
  // private string emailAddress;
  //
  public InputField emailText;
  public InputField usernameText;
  public InputField passwordText;
  //
  void Start()
  {
    
    // googleIdToken = "320930175122-jk18h4huuh1f76rfmc1qddfk81v2ocs8.apps.googleusercontent.com";
    // googleAccessToken = "i240fOOUr5nFSaJlPTvMBG7r";
    // auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    
  
    // auth.StateChanged += AuthStateChanged;
    // AuthStateChanged(this, null);
  }
  
  public void OnSignUp()
  {
    // var auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    string email = emailText.text;
    string password = passwordText.text;
    Firebase.Auth.FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
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
      Debug.LogFormat("Firebase user created successfully: {0} ({1})",
        newUser.DisplayName, newUser.UserId);
    });
  }
  //
  // public void onClick()
  // {
  //   var registerTask = auth.CreateUserWithEmailAndPasswordAsync(emailText.text, usenameText.text);
  //   yield return new WaitUntil(() => registerTask.IsCompleted);
  //   if (registerTask.Exception != null)
  //   {
  //     Debug.LogWarning($"Failed to register task with{registerTask.Exception}");
  //     OnUserRegistrationFailed.Invoke(registerTask.Exception);
  //   }
  //   else
  //   {
  //     Debug.Log($"Successfully registered user{registerTask.Result.Email}");
  //   }
  // }
  //
  // void AuthStateChanged(object sender, System.EventArgs eventArgs) {
  //   if (auth.CurrentUser != user) {
  //     bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
  //     if (!signedIn && user != null) {
  //       Debug.Log("Signed out " + user.UserId);
  //     }
  //     user = auth.CurrentUser;
  //     if (signedIn) {
  //       Debug.Log("Signed in " + user.UserId);
  //       displayName = user.DisplayName ?? "";
  //       emailAddress = user.Email ?? "";
  //       Debug.Log("Signed in email: " + emailAddress);
  //     }
  //   }
  // }
  //
  //
  // public void onClick()
  // {
  //   
  //   
  //   Firebase.Auth.Credential credential =
  //     Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
  //   auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
  //     if (task.IsCanceled) {
  //       Debug.LogError("SignInWithCredentialAsync was canceled.");
  //       return;
  //     }
  //     if (task.IsFaulted) {
  //       Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
  //       return;
  //     }
  //
  //     Firebase.Auth.FirebaseUser newUser = task.Result;
  //     Debug.LogFormat("User signed in successfully: {0} ({1})",
  //       newUser.DisplayName, newUser.UserId);
  //   });
  // }
  //
  // void authenticateFirebase(string googleIdToken, string googleAccessToken) {
  //   Firebase.Auth.Credential credential =
  //     Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
  //   auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
  //     if (task.IsCanceled) {
  //       Debug.LogError("SignInWithCredentialAsync was canceled.");
  //       return;
  //     }
  //     if (task.IsFaulted) {
  //       Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
  //       return;
  //     }
  //
  //     Firebase.Auth.FirebaseUser newUser = task.Result;
  //     Debug.LogFormat("User signed in successfully: {0} ({1})",
  //       newUser.DisplayName, newUser.UserId);
  //   });
  // }
  //

  
}
