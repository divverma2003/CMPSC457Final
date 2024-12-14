using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private int maxCoins;
    [SerializeField] private float minZRange;
    [SerializeField] private float maxZRange;

    private int coinsCollected;
    // Start is called before the first frame update
    private void Start()
    {
        InitialSpawn();
    }

    private void InitialSpawn()
    {
        int coinCount = Random.Range(0, maxCoins);
        for (int i = 0; i < coinCount; i++)
        {
            float randomZ = Random.Range(minZRange, maxZRange);
            Vector3 spawnPosition = new Vector3(spawnPos.position.x, 1f, randomZ);
            Instantiate(coin, spawnPosition, Quaternion.identity);
        }
    }
}
