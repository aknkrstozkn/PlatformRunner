using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject wall = null;
    [SerializeField] Transform player = null;
    [SerializeField] float zDistance = 20.0f;

    [SerializeField] Vector3 targetPos = Vector3.zero;
    [SerializeField] float speed = 0.5f;
    [SerializeField] float rotSpeed = 0.5f;
    
    public void FollowWall()
    {
        var lookAtTarget = new Vector3(wall.transform.position.x - transform.position.x, 
                            wall.transform.position.y - transform.position.y,
                            wall.transform.position.z - transform.position.z);
        var playerRot = Quaternion.LookRotation(lookAtTarget);


        transform.rotation = Quaternion.Slerp(transform.rotation, 
                playerRot, rotSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void FollowPlayer()
    {
        transform.position = new Vector3(transform.position.x,
                transform.position.y, 
                player.position.z - zDistance);
    }

    private void LateUpdate() {
        if(GameController.state == GameController.State.Race)
        {
            FollowPlayer();
        } 
        else if(GameController.state == GameController.State.Paint)
        {
            FollowWall();
        }
        
    }
}
