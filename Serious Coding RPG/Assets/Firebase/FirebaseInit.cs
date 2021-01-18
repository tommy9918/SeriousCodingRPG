using Firebase;
// using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync();
        // SceneManager.UnloadSceneAsync("SignUpScene");
        // SceneManager.UnloadSceneAsync("UploadUserProfile");
        // SceneManager.LoadScene("SignInScene", LoadSceneMode.Single);

        // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        // {
        //     FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        // });
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
