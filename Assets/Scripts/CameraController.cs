using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float distanceToPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y, 
            player.position.z - distanceToPlayer);
    }
}
