using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    public Text playerNameText;
    public Text playerHealthText;
    public GameObject playerListPrefab;
    public Transform playerListParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void InitializeLobby()
    {
        playerNameText.text = "Player: " + PhotonNetwork.LocalPlayer.NickName;
        // Implement logic to display other players in the lobby
    }

    public bool AllPlayersReady()
    {
        // Implement logic to check if all players are ready
        return true; // Replace with your actual implementation
    }
}
