using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public enum MotionState{ Idle, Running }
    
    [Header("Start Properties")]
    [SerializeField] public Vector3 targetPos = Vector3.zero;
    [SerializeField] public Vector3 startPos = Vector3.zero;
    [SerializeField] public Quaternion startRot = Quaternion.identity;
    public MotionState motionState{ get; set; }
    public void Init()
    {
        transform.position = startPos; 
        motionState = MotionState.Idle;
        transform.rotation = startRot;    
    }
}
