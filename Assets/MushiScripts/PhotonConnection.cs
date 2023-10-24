using Photon.Pun;
using UnityEngine;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to Photon master servers
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        PhotonNetwork.JoinLobby(); // Join a lobby after connecting to the master server
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("MyRoom"); // Creates a new room named "MyRoom"
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("MyRoom"); // Tries to join a room named "MyRoom"
    }
}
