using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
   // [SerializeField] Vector2 _clampValues = new Vector2(-12, 12);
    private Rigidbody _rb;
    private Vector3 _targetPos;
    private Vector3 _startPos;
    private Touch _touch;
    private string _currentState = "Idle";
    private string _newState = "Idle";
    void Start()
    {
        Input.simulateMouseWithTouches = true;

        _rb = GetComponent<Rigidbody>();
        _startPos = transform.position;
        _targetPos = transform.position;
    }

    
    void Update()
    {   /*
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
                SetTargetPosition();
            }
        }
                 

        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);

        float distance = Vector3.Distance(_targetPos, transform.position);
        _newState = distance < 0.1f ? "Idle" : "Running"; 
    }

    void SetTargetPosition()
    {
        //var ray = Camera.main.ScreenPointToRay(_touch.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform != null && hit.transform.tag != "Player")
            {
                _targetPos = hit.point;
                transform.LookAt(_targetPos);
            }
        }
    }

    void ChangeAnimState() 
    {
        if (_currentState == _newState) return;

        _currentState = _newState;
        GetComponent<Animator>().Play(_currentState);
    }


    private void LateUpdate() {
        ChangeAnimState();
    }

    void FixedUpdate()
    {
        //_newState = _rb.velocity.magnitude < 0.1f ? "Idle" : "Running";        
    }
}
