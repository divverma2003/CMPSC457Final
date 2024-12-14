using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [HideInInspector] public Vector3 currentPosition = new Vector3(-1, 0, 0);

    [SerializeField] private int minDistanceFromPlayer;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private Transform terrainHolder;

    private GameObject lastGeneratedTerrain; // To track the last terrain object
    private GameObject currentTerrain;
    private List<GameObject> activeTerrains = new List<GameObject>();
    private bool isFirstTerrain;


    // Start is called before the first frame update
    void Start()
    {
        isFirstTerrain = true;
        for (int i = 0; i < maxTerrainCount; i++) 
        {
            SpawnTerrain(true, new Vector3(-1, 0, 0));
        }
        maxTerrainCount += activeTerrains.Count;
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {
        if ((currentPosition.x - playerPos.x < minDistanceFromPlayer) || isStart)
        {
            int terrainChoice;
            // Decide which terrain we'll spawn
            if (isFirstTerrain)
            {
                terrainChoice = 0;
                isFirstTerrain= false;
            }
            else
            {
                terrainChoice = Random.Range(0, terrainDatas.Count);
            }
            
            // Decide how many of this terrain type we'll spawn
            int terrainInSuccession = Random.Range(1, terrainDatas[terrainChoice].maxInSuccession);

            // Spawn chosen terrain x amount of times
            for (int i = 0; i < terrainInSuccession; i++)
            {
                currentTerrain = terrainDatas[terrainChoice].possibleTerrain[Random.Range(0, terrainDatas[terrainChoice].possibleTerrain.Count)];

                if (lastGeneratedTerrain != null)
                {
                    // Check the X-scale of the last terrain
                    float previousXScale = lastGeneratedTerrain.transform.localScale.x;
                    float currentXScale = currentTerrain.transform.localScale.x;
                    if (Mathf.Approximately(previousXScale, 2f) && Mathf.Approximately(currentXScale, 2f)) // If Z-scale is 2
                    {
                        // Update currentPosition.x
                        currentPosition.x += 2f;
                    }
                    else if (Mathf.Approximately(previousXScale, 2f) ^ Mathf.Approximately(currentXScale, 2f))
                    {
                        // Update currentPosition.x
                        currentPosition.x += 1.5f;
                    }
                    else
                    {
                        // Update currentPosition.x
                        currentPosition.x += 1f;
                    }
                }
                else
                {
                    // Default increment for the first terrain
                    currentPosition.x += 1f;
                }

                lastGeneratedTerrain = currentTerrain;

                // Spawn the new terrain
                // currentTerrain = Instantiate(terrainDatas[terrainChoice].terrain, currentPosition, Quaternion.identity, terrainHolder);
                currentTerrain = Instantiate(currentTerrain, currentPosition, Quaternion.identity, terrainHolder);
                activeTerrains.Add(currentTerrain);
                if (!isStart)
                {
                    // Destroy first terrain if the terrain count exceeds the max
                    if (activeTerrains.Count > maxTerrainCount)
                    {
                        Destroy(activeTerrains[0]);
                        activeTerrains.RemoveAt(0);
                    }
                }
            }
        }
    }
}

