using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeperationTime;
    [SerializeField] private float maxSeperationTime;
    [SerializeField] private bool isRightSide;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    // Update is called once per frame
    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeperationTime, maxSeperationTime));
            GameObject go = Instantiate(objectToSpawn, spawnPos.position, Quaternion.identity);
            
            if (isRightSide) {
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}
