using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text;

public class CustomRoomManager : MonoBehaviourPunCallbacks
{
    private PlayerCountDisplay playerCountDisplay;
    private UserManager userManager;
    private const string lobbyNamePrefix = "Lobby_";
    private const string roomCodeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int roomCodeLength = 6;

    [SerializeField]
    private TMP_InputField roomCodeInput;

    private void Start()
    {
        playerCountDisplay = GetComponent<PlayerCountDisplay>();
        userManager = GetComponent<UserManager>();
        ConnectToMaster();
    }

    private void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        // When connected to the master server, join the lobby
        JoinOrCreateLobby();
    }

    private void JoinOrCreateLobby()
    {
        if (roomCodeInput != null && !string.IsNullOrEmpty(roomCodeInput.text))
        {
            // If there is a room code input, try joining that specific room
            JoinRoom(roomCodeInput.text.ToUpper());
        }
        else
        {
            // If no room code input, create a random room
            CreateRandomRoom();
        }
    }

    private void JoinRoom(string roomCode)
    {
        string lobbyName = lobbyNamePrefix + roomCode;
        TypedLobby myLobby = new TypedLobby(lobbyName, LobbyType.Default);
        PhotonNetwork.JoinLobby(myLobby);
    }

    private void CreateRandomRoom()
    {
        string randomRoomCode = GenerateRandomRoomCode(roomCodeLength);
        string roomName = "Room_" + randomRoomCode;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        // No specific action needed here as the room is already joined or created
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room. Room name: " + PhotonNetwork.CurrentRoom.Name);
        playerCountDisplay.UpdatePlayerCount();

        if (PhotonNetwork.IsMasterClient)
        {
            // Only the room creator can start the game
            // Implement game start logic here
        }
    }

    private string GenerateRandomRoomCode(int length)
    {
        StringBuilder roomCodeBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, roomCodeCharacters.Length);
            roomCodeBuilder.Append(roomCodeCharacters[index]);
        }

        return roomCodeBuilder.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Example: Start the game when the user presses the space key
            if (PhotonNetwork.IsMasterClient)
            {
                // Implement game start logic here
            }
        }
    }
}
