using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public Text roomCodeText;
    public GameObject lobbyPanel;
    public GameObject gameplayPanel;
    public Button readyButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // Implement logic for UI, e.g., showing the main menu
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public void JoinGame(string roomCode)
    {
        PhotonNetwork.JoinRoom(roomCode);
    }

    public override void OnJoinedRoom()
    {
        roomCodeText.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;
        LobbyManager.Instance.InitializeLobby();
    }

    public void Ready()
    {
        readyButton.interactable = false;
        photonView.RPC("PlayerReady", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void PlayerReady()
    {
        if (LobbyManager.Instance.AllPlayersReady())
        {
            photonView.RPC("LoadGameplayScene", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void LoadGameplayScene()
    {
        lobbyPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        PhotonNetwork.LoadLevel("GameplayScene");
    }
}
