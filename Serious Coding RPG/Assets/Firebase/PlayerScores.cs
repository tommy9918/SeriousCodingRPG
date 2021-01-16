using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;
using FullSerializer;
using Firebase.Auth;
using Firebase;


public class PlayerScores : MonoBehaviour
{
    
    public Text scoreText;
    
    public InputField getScoreText;
    private System.Random random = new System.Random();
    
    

    public InputField nameText;
    private User user = new User();

    public static int playerScore;
    public static string playerName;
    
    
    private string databaseURL = "https://spelloverflow-default-rtdb.firebaseio.com/"; 
    private string AuthKey = "AIzaSyDh9qM2YHYAwd-F6uEwW6LmJj4p3yotduc";
    public InputField emailText;
    public InputField usernameText;
    public InputField passwordText;
    public static fsSerializer serializer = new fsSerializer();
    private string idToken;
    
    public static string localId;

    private string getLocalId;

    void Start()
    {
        playerScore = random.Next(0, 101);
        scoreText.text = "Score:" + playerScore;
    }

    public void OnSubmit() {
        playerScore = random.Next(0, 101);
        playerName = nameText.text;
        PostToDatabase();
    }

    public void OnGetScore() {
        RetrieveFromDatabase();

    }

    private void PostToDatabase(bool emptyScore = false)
    {
        User user = new User();

        if (emptyScore)
        {
            user.userScore = 0;
        }
        
        RestClient.Put(databaseURL + "/" + playerName + ".json" , user);
    }

    public void RetrieveFromDatabase() {
        RestClient.Get<User>("https://spelloverflow-default-rtdb.firebaseio.com/" + nameText.text + ".json").Then(response => {
            user = response;
            scoreText.text = user.userScore.ToString();
        });
        //return null;

    }
    
    public void SignUpUserButton()
    {
        // SignUpUser(emailText.text, usernameText.text, passwordText.text);
        
    }
    
    public void SignInUserButton()
    {
        // SignInUser(emailText.text, passwordText.text);
    }
    
    private void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                playerName = username;
                PostToDatabase(true);
                
            }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
    
    private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                GetUsername();
            }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    private void GetUsername()
    {
        RestClient.Get<User>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            playerName = response.userName;
        });
    }
    
    private void GetLocalId(){
        RestClient.Get(databaseURL + ".json?auth=" + idToken).Then(response =>
        {
            var username = getScoreText.text;
            
            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string, User> users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users.Values)
            {
                if (user.userName == username)
                {
                    getLocalId = user.localId;
                    RetrieveFromDatabase();
                    break;
                }
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }


    

}
