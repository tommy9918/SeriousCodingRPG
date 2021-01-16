using Firebase;
// using Firebase.Analytics;
using UnityEngine;

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

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
