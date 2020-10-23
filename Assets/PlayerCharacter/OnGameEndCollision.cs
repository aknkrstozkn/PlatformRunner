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
            playerController.Init();
            playerController._moveState = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if(other.collider.CompareTag("Finish"))
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
            playerController.enabled = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerController.Init();
            
            playerController.SetTargetPosition(finishArea.transform.position);
        }
        else if(other.collider.CompareTag("RotatingPlatform"))
        {
            transform.parent = other.transform;
        }
        else if(other.collider.CompareTag("RotatingStick"))
        {
            playerController._moveState = false;
            
            Vector3 dir = other.transform.localPosition - transform.position;
            dir = -dir.normalized;   
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
