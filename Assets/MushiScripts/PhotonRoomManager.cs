using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    private PlayerCountDisplay playerCountDisplay;

    private void Start()
    {
        playerCountDisplay = GetComponent<PlayerCountDisplay>();

        // Connect to the Photon master server (Only one call is needed)
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");

        // Join the default lobby
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to lobby");
    }

    // This callback method is executed once you've joined the lobby
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined a lobby!");

        // Now, try to join a random room
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a random room!");

        // If we failed to join a random room, create a new one
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room!");

        playerCountDisplay.UpdatePlayerCount();
    }
}
