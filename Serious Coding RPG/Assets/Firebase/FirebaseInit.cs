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
        
        // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        // {
        //     FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        // });
    }

 
}
