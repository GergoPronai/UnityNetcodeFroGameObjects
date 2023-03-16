using UnityEngine;
using Unity.Netcode;
public class EnemyNetwork : NetworkBehaviour
{
    private EnemyHealthManager healthManager;

    private void Start()
    {
        // Get the EnemyHealthManager instance
        healthManager = EnemyHealthManager.Instance;
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            TransmitState();
        }
        else
        {
            ConsumeState();
        }
    }

    private void TransmitState()
    {
        healthManager.EnemyHealth.Value = transform.GetComponent<EnemyScript>().healthManager.EnemyHealth.Value;
    }

    private void ConsumeState()
    {
        if (healthManager.EnemyHealth.Value != transform.GetComponent<EnemyScript>().healthManager.EnemyHealth.Value)
        {
            transform.GetComponent<EnemyScript>().healthManager.EnemyHealth.Value = healthManager.EnemyHealth.Value;
        }
    }
}
