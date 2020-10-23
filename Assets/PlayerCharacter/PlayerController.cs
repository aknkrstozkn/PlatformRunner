using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
    [SerializeField] float _rotSpeed = 5f;
    private Rigidbody _rb;
    public Vector3 _targetPos;
    private Vector3 _lookAtTarget;
    private Quaternion _playerRot;
    private Vector3 _startPos;
    public bool _moveState = false;
    /* Mobile Control
    private Touch _touch; */
    private string _currentState = "Idle";
    private string _newState = "Idle";
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.sleepThreshold = 0.0f;
        Init();
    }

    
    void Update()
    {   
        InputControl();

        Move();        
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
        if (Input.GetMouseButtonDown(0))
        {
            /* Mobile control
            SetTargetPosition(getRayHitPoint(_touch.position); */
            SetTargetPosition(GetRayHitPoint(Input.mousePosition));            
        }
        else if(Input.GetMouseButton(0)) 
        {
            if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            {           
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
                _moveState = true;   
                return hit.point;                     
            }
        }

        _moveState = false;
        return transform.position;
    }
    public void SetTargetPosition(Vector3 targetPos)
    {   
        if (targetPos.Equals(transform.position) || !_moveState) return;

        _targetPos = targetPos;
        //transform.LookAt(_targetPos);
        _lookAtTarget = new Vector3(_targetPos.x - transform.position.x, 
                            transform.position.y,
                            _targetPos.z - transform.position.z);
        _playerRot = Quaternion.LookRotation(_lookAtTarget);
    }
    public void Move()
    {       
        if(_moveState)
        {   
            // Rotate
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                _playerRot, _rotSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
            float distance = Vector3.Distance(_targetPos, transform.position);
            _moveState = distance < 0.1f ? false : _moveState;
        }
    }

    public void Init()
    {
        _startPos = transform.position;
        _targetPos = transform.position;
        _playerRot = transform.rotation;        
    }

    public void ChangeAnimState() 
    {
        _newState = _moveState ? "Running" : "Idle";

        if (_currentState == _newState) return;

        _currentState = _newState;
        GetComponent<Animator>().Play(_currentState);
    }


    private void LateUpdate() {
        ChangeAnimState();
    }
}
