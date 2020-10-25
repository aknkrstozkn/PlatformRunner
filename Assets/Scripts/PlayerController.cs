using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _rotSpeed = 5f;
    private Rigidbody _rb;
    private Quaternion _playerRot;
    private Vector3 _lookAtTarget;

    void Start()
    {
        Init();

        _rb = GetComponent<Rigidbody>();
        _rb.sleepThreshold = 0.0f;
    }
    public new void Init()
    {
        base.Init();
        _playerRot = transform.rotation;        
    }
    void Update()
    {   
        if(GameController.state == GameController.State.Race)
        {
            InputControl();
            Move();
        }
        else
        {
            enabled = false;
        }                
    }
    void InputControl()
    {
        /* Mobile Control, for only mobile because this is more efficient
        if (Input.touchCount > 0)
        {   
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
               SetTargetPosition();      
                
            }
        }
        */
        if(Input.GetMouseButton(0)) 
        {   
            if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            {    
                /* Mobile control
                SetTargetPosition(getRayHitPoint(_touch.position); */       
                SetTargetPosition(GetRayHitPoint(Input.mousePosition));   
            }
        }
    }
    private Vector3 GetRayHitPoint(Vector2 position)
    {        
        var ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform != null && hit.transform.tag != "Player")
            {   
               return hit.point;                     
            }
        }
        return transform.position;
    }
    public void SetTargetPosition(Vector3 targetPos)
    {   
        if (targetPos.Equals(transform.position)) return;

        this.targetPos = targetPos;
        _lookAtTarget = new Vector3(targetPos.x - transform.position.x, 
                            transform.position.y,
                            targetPos.z - transform.position.z);
        _playerRot = Quaternion.LookRotation(_lookAtTarget);
    }
    public void Move()
    {       
        // Rotate
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            _playerRot, _rotSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
        
        float distance = Vector3.Distance(targetPos, transform.position);
        ChangeMotionState(distance < 0.1f ? MotionState.Idle : MotionState.Running);
    }
    public void ChangeMotionState(MotionState _motionState) 
    {
        if (motionState == _motionState) {return;}

        motionState = _motionState;
        GetComponent<Animator>().Play(motionState.ToString());
    }
}
