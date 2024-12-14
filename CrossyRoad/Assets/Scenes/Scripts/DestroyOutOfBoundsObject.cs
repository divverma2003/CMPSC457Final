using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Define the z-range in which the object must remain.
    private float minZ = -25f;
    private float maxZ = 25f;

    private void Update()
    {
        // If the object moves beyond the defined bounds, destroy it.
        if (transform.position.z < minZ || transform.position.z > maxZ)
        {
            Destroy(gameObject);
        }
    }
}

