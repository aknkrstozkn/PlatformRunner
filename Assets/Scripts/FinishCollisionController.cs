using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollisionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        
        if(other.collider.CompareTag("Player"))
        {
            GameController.state = GameController.State.Paint;
        }
        else if(other.collider.CompareTag("Opponent"))
        {
            GameController.state = GameController.State.GameOver;            
        }
    }
}
