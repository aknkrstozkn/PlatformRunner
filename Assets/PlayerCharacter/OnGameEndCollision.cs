using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameEndCollision : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI = null;
    [SerializeField] GameObject finishArea = null;
    [SerializeField] float pushForce = 30f;
    private Vector3 _startPos;
    private bool isGameOver;
    private PlayerController playerController;
    private void Start() 
    {
        isGameOver = false;
        _startPos = transform.position;
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("MandatoryObstacle") || other.collider.CompareTag("Water"))
        {
            // Respawn at beginning
            transform.position = _startPos;
        }
        else if(other.collider.CompareTag("Finish"))
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
            playerController.enabled = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Vector3 targetPos = finishPlatform.transform.position;
            //targetPos.y = transform.position.y;
            playerController.SetTargetPosition(finishArea.transform.position);
        }
        else if(other.collider.CompareTag("RotatingPlatform"))
        {
            transform.parent = other.transform;
        }
        else if(other.collider.CompareTag("RotatingStick"))
        {
            
            // If the object we hit is the enemy
            // Calculate Angle Between the collision point and the player
            //Vector3 dir = other.contacts[0].point - transform.position;
            Vector3 dir = other.transform.localPosition - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;

            //var forceVec = other.GetComponent<Rigidbody>().velocity.normalized * pushForce;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("RotatingPlatform"))
        {
            this.transform.parent = null;
        }            
    }

    private void Update() {
        if (isGameOver)
        {
            playerController.Move();
        }
    }

    private void LateUpdate() {
        if (isGameOver)
        {
            playerController.ChangeAnimState();
        }
    }
}
