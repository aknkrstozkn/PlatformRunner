using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] float zDistance = 20.0f;

    private void LateUpdate() {
        transform.position = new Vector3(transform.position.x,
            transform.position.y, 
            player.position.z - zDistance);
    }
}
