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

    public TextMeshProUGUI playerCount;

    private Dictionary<string, string> playerNames = new Dictionary<string, string>();

    public TMP_Text roomCodeText;
    public TMP_Text playerJoinPrompt;
    public GameObject lobbyPanel;
    public GameObject roomPanel; 
    public GameObject gameplayPanel;

    public GameObject dummyPanel;

    //public Button readyButton;
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
        //PhotonNetwork.JoinRoom(newRoomCode);

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
        string displayName;

        if (userManager == null)
        {
            displayName = "TestUser"; // Set the default display name to "test" if it's null or empty
        }
        else
        {
            displayName = userManager.GetUserDisplayName();
        }
        PhotonNetwork.NickName = displayName;

        photonView.RPC("UpdatePlayerCustomProperties_RPC", RpcTarget.AllBuffered);
        photonView.RPC("UpdatePlayerList_RPC", RpcTarget.AllBuffered);
        photonView.RPC("CalculateAndSetPlayerCount", RpcTarget.AllBuffered);

        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            photonView.RPC("LoadGameplayScene", RpcTarget.AllBuffered);
        }
        dummyPanel.SetActive(true);
    }

    [PunRPC]
    public void CalculateAndSetPlayerCount()
    {
        Debug.Log("in CalculateAndSetPlayerCount");
        int playerCountInt = PhotonNetwork.PlayerList.Length;
        string playerCountStr = $"playercount: {playerCountInt}";
        if (playerCount != null)
        {
            playerCount.text = playerCountStr;
            Debug.Log($"Player Count Text Updated: {playerCountStr}");
        }
        else
        {
            Debug.LogError("Player Count Text component not assigned in the Inspector.");
        }

        Debug.Log($"Player Count: {playerCountInt}");
    }

    [PunRPC]
    private void UpdatePlayerCustomProperties_RPC()
    {
        UpdatePlayerCustomProperties();
    }

    [PunRPC]
    private void UpdatePlayerList_RPC()
    {
        UpdatePlayerList();
    }



    [PunRPC]
    public void PlayerReady()
    {
        Debug.Log($"[GameManager] PlayerReady RPC called on {PhotonNetwork.NickName}");

        // Set the player's "isReady" status to true
        SetPlayerReadyStatus(true);

        if (AreAllPlayersReady())
        {
            Debug.Log("[GameManager] All players are ready. Activating gameplay panel."); 
        }
        else
        {
            Debug.Log("[GameManager] Waiting for all players to be ready.");
        }
    }

    private void SetPlayerReadyStatus(bool isReady)
    {
        if (PhotonNetwork.LocalPlayer != null)
        {
            // Update the player's "isReady" status in custom properties
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
            customProperties["IsPlayerReady"] = isReady;
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
        }
    }

    private bool AreAllPlayersReady()
    {
        bool allPlayersReady = true;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if (player.CustomProperties.TryGetValue("IsPlayerReady", out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    allPlayersReady = false;
                    Debug.Log($"[GameManager] Player {player.NickName} is not ready.");
                }
                else
                {
                    Debug.Log($"[GameManager] Player {player.NickName} is ready.");
                }
            }
            else
            {
                // If any player hasn't set their ready status yet
                allPlayersReady = false;
                Debug.Log($"[GameManager] Player {player.NickName} has not set their ready status.");
            }
        }

        if (allPlayersReady)
        {
            Debug.Log("[GameManager] All players are ready. Activating gameplay panel.");
            dummyPanel.SetActive(true);
            dummyPanel.SetActive(false);
            gameplayPanel.SetActive(true);
        }
        else
        {
            Debug.Log("[GameManager] Waiting for all players to be ready.");
        }

        return allPlayersReady;
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("IsPlayerReady"))
        {
            Debug.Log($"[GameManager] Player {targetPlayer.NickName}'s ready status updated.");
            
            // If this update makes all players ready, activate the gameplay panel
            if (AreAllPlayersReady())
            {
                Debug.Log("[GameManager] All players are now ready. Activating gameplay panel.");
                gameplayPanel.SetActive(true);
            }
        }
    }

    private void UpdatePlayerList()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            StringBuilder playerListBuilder = new StringBuilder();

            foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                string playerName = player.NickName;
                int hitCount = player.CustomProperties.ContainsKey("HitCount") ? (int)player.CustomProperties["HitCount"] : 0;
                
                playerListBuilder.AppendLine($"{playerName}: {hitCount}");
            }

            playerListText.text = playerListBuilder.ToString();
        }
    }

    private void UpdatePlayerCustomProperties()
    {
        if (PhotonNetwork.LocalPlayer != null)
        {
            // Set the initial hit count to 0
            ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
            customProperties["HitCount"] = 0;
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
        }
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
