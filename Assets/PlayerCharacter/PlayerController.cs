using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
    [SerializeField] float _rotSpeed = 5f;
    private Rigidbody _rb;
    private Vector3 _targetPos;
    private Vector3 _lookAtTarget;
    private Quaternion _playerRot;
    private Vector3 _startPos;
    /* Mobile Control
    private Touch _touch; */
    private string _currentState = "Idle";
    private string _newState = "Idle";
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPos = transform.position;
        _targetPos = transform.position;
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
            SetTargetPosition();
        }
        else if(Input.GetMouseButton(0)) 
        {
            if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            {
                SetTargetPosition();
            }
        }
    }
    void SetTargetPosition()
    {   /* Mobile control
        var ray = Camera.main.ScreenPointToRay(_touch.position); */
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform != null && hit.transform.tag != "Player")
            {
                _targetPos = hit.point;
                //transform.LookAt(_targetPos);
                _lookAtTarget = new Vector3(_targetPos.x - transform.position.x, 
                    transform.position.y,
                    _targetPos.z - transform.position.z);
                _playerRot = Quaternion.LookRotation(_lookAtTarget);
            }
        }
    }
    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            _playerRot, _rotSpeed * Time.deltaTime);
        
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);

    }

    void ChangeAnimState() 
    {
        float distance = Vector3.Distance(_targetPos, transform.position);
        _newState = distance < 0.1f ? "Idle" : "Running";

        if (_currentState == _newState) return;

        _currentState = _newState;
        GetComponent<Animator>().Play(_currentState);
    }


    private void LateUpdate() {
        ChangeAnimState();
    }

    void FixedUpdate()
    {
        // No Physic calc yet.     
    }
}
