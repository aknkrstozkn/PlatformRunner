using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        
        if(other.collider.CompareTag("Player") || other.collider.CompareTag("Opponent"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.collider.CompareTag("Player") || other.collider.CompareTag("Opponent"))
        {
            other.transform.parent = null;
        }                   
    }
}
