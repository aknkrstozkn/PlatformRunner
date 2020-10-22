using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleController : MonoBehaviour
{
    [SerializeField] float axisDistance = 24f;
    [SerializeField] float speed = 0.1f;
    private float _targetXPos;
    // Start is called before the first frame update
    void Start()
    {   
        axisDistance = -axisDistance;
        _targetXPos = transform.position.x - axisDistance;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, _targetXPos, speed * Time.deltaTime), 
                transform.position.y, transform.position.z);

        if ((int)_targetXPos == (int)transform.position.x)
        {
            axisDistance = -axisDistance;
            _targetXPos = transform.position.x - axisDistance;
        }
    }
}
