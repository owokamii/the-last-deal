using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject sellerPrefab;
    [SerializeField] private Transform[] sellerSpawnPoints;
    
    private void Start()
    {
        SpawnInitialSellers();
    }

    private void SpawnInitialSellers()
    {
        foreach (var spawnpoint in sellerSpawnPoints)
        {
            Instantiate(sellerPrefab, spawnpoint.position, Quaternion.identity);
        }
    }
}
