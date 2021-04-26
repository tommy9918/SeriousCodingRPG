using Firebase;
// using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseInit : MonoBehaviour
{
    private Firebase.Auth.FirebaseAuth auth;
    private static Firebase.Auth.FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync();
        
    }
    
}
