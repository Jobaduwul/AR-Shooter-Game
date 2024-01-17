using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

using ExitGames.Client.Photon;

using System.Collections.Generic;



public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    private UserManager userManager;

    public PhotonView photonView;

    public TextMeshProUGUI playerListText;

    private Dictionary<string, string> playerNames = new Dictionary<string, string>();

    public TMP_Text roomCodeText;
    public TMP_Text playerJoinPrompt;
    public GameObject lobbyPanel;
    public GameObject roomPanel; 
    public GameObject gameplayPanel;

    public GameObject dummyPanel;

    public Button readyButton;
    private const int roomCodeLength = 6;
    public TMP_InputField roomCodeInput;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        photonView = GetComponent<PhotonView>(); 

        dummyPanel.SetActive(false);
        gameplayPanel.SetActive(false);

        // Find UserManager in the scene and assign it
        userManager = FindObjectOfType<UserManager>();

        if (userManager == null)
        {
            Debug.Log("UserManager not found in the scene.");
        }
        else
        {
            Debug.Log("UserManager found and assigned in GameManager.");
        }

        playerListText = lobbyPanel.GetComponentInChildren<TextMeshProUGUI>();
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
        Debug.Log("Connected to Master");
    }

    public void CreateGame()
    {
        Debug.Log("Starting to join Room");
        string newRoomCode = GenerateRandomRoomCode(roomCodeLength);

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 3,
            CustomRoomProperties = new Hashtable
            {
                { "AutoSyncScene", true }
            },
            CustomRoomPropertiesForLobby = new[] { "AutoSyncScene" }
        };

        PhotonNetwork.CreateRoom(newRoomCode, roomOptions);
        PhotonNetwork.JoinRoom(newRoomCode);

        UIhandler(newRoomCode);
    }


    public void JoinGame()
    {
        string roomCode = roomCodeInput.text;
        
        if (string.IsNullOrEmpty(roomCode))
        {
            Debug.LogError("Room code is null or empty. Please enter a valid room code.");
        }
        else
        {
            roomCode = roomCode.ToUpper();
            Debug.Log("To Join room " + roomCode);
            PhotonNetwork.JoinRoom(roomCode);

            UIhandler(roomCode);
        }
    }


    public void ForDebug()
    {
        //for debug only
        string roomCode = roomCodeInput.text.ToUpper();
        Debug.Log("The room is" + roomCode);
        
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerCustomProperties(); 
        //Debug.Log(" in joined room , done executing 'UpdatePlayerCustomProperties' ");// Update custom properties when a player joins
        UpdatePlayerList();

        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            photonView.RPC("LoadGameplayScene", RpcTarget.AllBuffered);
        }
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


    // public override void OnPlayerEnteredRoom(Player newPlayer)
    // {
    //     Debug.Log("In 'OnPlayerEnteredRoom' ");
    //     UpdatePlayerList();
    //     UpdatePlayerCustomProperties(); // Update custom properties when a player joins
    // }

    // public override void OnPlayerLeftRoom(Player otherPlayer)
    // {
    //     Debug.Log("In 'onPlayerLeftRoom' ");
    //     UpdatePlayerList();
    //     UpdatePlayerCustomProperties(); // Update custom properties when a player leaves
    // }

    private void UpdatePlayerList()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            StringBuilder playerList = new StringBuilder();

            if (PhotonNetwork.PlayerList.Length == 0)
            {
                Debug.Log("Player list is empty.");
            }
            else
            {
                string temp_user = userManager.GetUserDisplayName();
                if(temp_user != null){
                    Debug.Log("Player list updated. Players in the room: \n" + temp_user);
                }
                else{
                    Debug.Log("temp user is empty" );
                }
            }
        }
        else
        {
            Debug.LogWarning("CurrentRoom is null. Cannot update player list.");
        }
    }

    
    private void UpdatePlayerCustomProperties()
    {
        Debug.Log(" in 'UpdatePlayerCustomProperties' ");
        string localPlayerId = PhotonNetwork.LocalPlayer.UserId;
        string temp_user = userManager.GetUserDisplayName();

        // Add the player to the local dictionary (for demonstration purposes)
        playerNames.Add(localPlayerId, temp_user);
        Debug.Log(" added in dictionary ");

        // Create a hashtable to store custom properties
        ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();

        // Add custom properties
        playerCustomProperties.Add("DisplayName", temp_user); // Using temp_user instead of localPlayerId for display name
        playerCustomProperties.Add("Health", 10); // Assuming Health is an integer property

        Debug.Log(" added custom properties ");

        // Set custom properties for the local player
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

        // Debug logs for information
        Debug.Log("Updated custom properties for local player:");
        Debug.Log("Player ID: " + localPlayerId);
        Debug.Log("Display Name: " + temp_user);
        Debug.Log("Health: " + PhotonNetwork.LocalPlayer.CustomProperties["Health"]); // Log the current Health property
    }

    [PunRPC]
    private void LoadGameplayScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            roomPanel.SetActive(false);
            //gameplayPanel.SetActive(true);
            //PhotonNetwork.LoadLevel("GameplayScene");
        }
    }

    private string GenerateRandomRoomCode(int length)
    {
        StringBuilder roomCodeBuilder = new StringBuilder();
        const string roomCodeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, roomCodeCharacters.Length);
            roomCodeBuilder.Append(roomCodeCharacters[index]);
        }

        return roomCodeBuilder.ToString();
    }

    public void UIhandler(string RoomCode)
    {
        Debug.Log("Room Joined" + RoomCode);
        roomCodeText.text = "Room Code: " + RoomCode;
        roomPanel.SetActive(false);
        //to be deleted later
        dummyPanel.SetActive(true);
    }
}
