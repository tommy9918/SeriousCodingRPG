using System;

using Firebase.Auth;


[Serializable]
public class UserProfile
{
    public string name;
    public int score;
    public int level;
    public string uid;
    public string email;
    
    public UserProfile(FirebaseUser user)
    {
        name = user.DisplayName;
        email = user.Email;
        uid = user.UserId;
    }


    public void setScore(int parse)
    {
        score = parse;
    }

    public void setLevel(int parse)
    {
        level = parse;
    }
}
