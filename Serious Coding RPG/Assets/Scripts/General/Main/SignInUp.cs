using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInUp : MonoBehaviour
{
    public GameObject sign_in;
    public GameObject sign_up;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToSignIn()
    {
        sign_in.SetActive(true);
        sign_up.SetActive(false);
    }

    public void SwitchToSignOut()
    {
        sign_in.SetActive(false);
        sign_up.SetActive(true);
    }
}
