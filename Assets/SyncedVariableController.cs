using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class SyncedVariableController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int bossHealth = 100;
    private PhotonView photonview;

    [SerializeField]
    private TextMeshProUGUI bossHealthText;

    void Start()
    {
        photonview = GetComponent<PhotonView>();
        UpdateBossHealthText();
    }

    public void onFire()
    {
        bossHealth--;
        UpdateBossHealthText();
        photonview.RPC("SyncVariable", RpcTarget.OthersBuffered, bossHealth);
    }

    private void UpdateBossHealthText()
    {
        bossHealthText.text = bossHealth.ToString();
    }

    [PunRPC]
    private void SyncVariable(int updatedHealth)
    {
        bossHealth = updatedHealth;
        UpdateBossHealthText();
    }
}
