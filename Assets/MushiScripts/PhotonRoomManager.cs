using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    private PlayerCountDisplay playerCountDisplay;
    private const string roomName = "TheOnlyRoom"; // Fixed room for now

    private void Start()
    {
        playerCountDisplay = GetComponent<PlayerCountDisplay>();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join the fixed room. The room was: " +roomName);

        // If failed, creating the room
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room. room name:" + roomName);

        playerCountDisplay.UpdatePlayerCount();
    }
}
