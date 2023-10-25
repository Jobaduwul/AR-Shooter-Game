using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; 
    public float maxHealth;
    private float currentHealth;
    private SyncedVariableController syncedVariableController;

    private void Start()
    {
        syncedVariableController = GetComponent<SyncedVariableController>();
        healthSlider.maxValue = syncedVariableController.maxBossHealth;
        currentHealth = syncedVariableController.bossHealth;

        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        healthSlider.value = syncedVariableController.bossHealth;
    }
}
