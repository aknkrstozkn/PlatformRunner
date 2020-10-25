using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionController : MonoBehaviour
{
    [SerializeField] bool kill = false;
    [SerializeField] bool push = false;
    [SerializeField] float pushForce = 20f;

    private CharacterController characterController;
    
    // Respawn at Character
    void Kill(Collision collision)
    {
        characterController = collision.gameObject.GetComponent<CharacterController>();
        characterController.Init();
    }
    // Push character with the pushForce
    void Push(Collision collision)
    {
        characterController = collision.gameObject.GetComponent<CharacterController>();
        characterController.motionState = CharacterController.MotionState.Idle;            
        Vector3 dir = collision.transform.position - transform.localPosition;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * pushForce, ForceMode.VelocityChange);
        var powerMult = (pushForce / 10);
        var impectVector = dir.normalized * powerMult;
        var newTargetPos = new Vector3(collision.transform.position.x + impectVector.x, 
                                collision.transform.position.y,
                                collision.transform.position.z + impectVector.z);
        collision.gameObject.GetComponent<CharacterController>().targetPos = newTargetPos;                                   
    }

    private void OnCollisionEnter(Collision collision) 
    {        
        if(kill)
        {                
            Kill(collision);  
        }
        else if(push)
        {
            Push(collision);
        }
    }
}
