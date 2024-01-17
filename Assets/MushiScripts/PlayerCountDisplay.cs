using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerCountDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerCountText;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        UpdatePlayerCount();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCount();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount();
    }

    public void UpdatePlayerCount()
    {
        if (photonView.IsMine)
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            playerCountText.text = $"Number of Players: {playerCount}";
        }
    }
}
