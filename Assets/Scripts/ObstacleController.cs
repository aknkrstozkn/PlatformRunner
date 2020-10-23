using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] bool move = false;
    [SerializeField] Vector3 targetPos = new Vector3(0f, 0f, 0f);
    [SerializeField] float speed = 0.1f;
    [Header("Rotation")]
    [SerializeField] bool rotate = false;
    [SerializeField] Vector3 rotation = new Vector3(0f, 1f, 0f);
    [SerializeField] float rotationSpeed = 0.1f;
    
    private float timeMultp = 0f;
    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {   
        _startPos = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Move(targetPos, speed);
        }
        
        if(rotate)
        {
            Rotate(rotation, rotationSpeed);
        }
    }

    public void Move(Vector3 targetPos, float speed)
    {
        timeMultp += speed * Time.deltaTime;

        transform.localPosition = Vector3.Lerp(_startPos,
        targetPos, timeMultp);

        if (targetPos.Equals(transform.localPosition))
        {
            this.targetPos = _startPos;
            _startPos = transform.localPosition;

            timeMultp = 0f;
        }
    }

    public void Rotate(Vector3 rotation, float rotationSpeed)
    {
        if (rotate)
        {
            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        }
    }
}
