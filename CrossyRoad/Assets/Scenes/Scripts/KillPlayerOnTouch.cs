using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillPlayerOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            // Destroy the player object after a 1 second delay
            Destroy(collision.gameObject, 0.25f);
        }
    }
}
