using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness;
    // Start is called before the first frame update


    // Update is called once per frame
    private void Update()
    {
        if (player != null) 
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, smoothness);
        }
    }
}
