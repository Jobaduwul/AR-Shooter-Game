using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        photonView.RPC("UpdateHealth", RpcTarget.AllBuffered, damage);
    }

    [PunRPC]
    private void UpdateHealth(int damage)
    {
        currentHealth -= damage;
        // Implement logic to check for winning/losing conditions
        // and update UI with new health values
    }
}
