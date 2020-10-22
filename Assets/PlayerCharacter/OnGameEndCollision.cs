using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameEndCollision : MonoBehaviour
{
    private Vector3 _startPos;

    private void Start() 
    {
        _startPos = transform.position;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("MandatoryObstacle"))
        {
            // Respawn at beginning
            transform.position = _startPos;
        }
    }
}
