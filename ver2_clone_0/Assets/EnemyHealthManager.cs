using Unity.Netcode;
using UnityEngine;

public class EnemyHealthManager : NetworkBehaviour
{
    public static EnemyHealthManager Instance;

    public NetworkVariable<float> EnemyHealth = new NetworkVariable<float>(100f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
