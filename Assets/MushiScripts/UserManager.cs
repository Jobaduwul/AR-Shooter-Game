using UnityEngine;
using Firebase.Auth;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance;

    public FirebaseUser currentUser;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentUser(FirebaseUser user)
    {
        currentUser = user;

        if (currentUser != null)
        {
            Debug.Log("This is the UserManager. User Id: " + currentUser.UserId + " User Name: " + currentUser.DisplayName);
        }
        else
        {
            Debug.Log("UserManager: CurrentUser is null.");
        }
    }
}
