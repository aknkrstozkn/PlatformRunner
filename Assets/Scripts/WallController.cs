using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallController : MonoBehaviour
{    
    [SerializeField] GameObject gameOverUI = null;
    [SerializeField] Vector3 targetPos = Vector3.zero;
    [SerializeField] public float speed = 0;

    private Vector3 _startPos;
    private float _timeMultp = 0;

    private int _travelCount = 2;

    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            Move();
        }
        

        if(_travelCount <= 0)
        {
            gameOverUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = 
                    "Game Over!";
            gameOverUI.SetActive(true);
            this.enabled = false;
        }
    }

    public void Move()
    {
        _timeMultp += speed * Time.deltaTime;

        transform.localPosition = Vector3.Lerp(_startPos,
        targetPos, _timeMultp);

        if (targetPos.Equals(transform.localPosition))
        {
            this.targetPos = _startPos;
            _startPos = transform.localPosition;

            _timeMultp = 0f;
            transform.GetChild(1).gameObject.SetActive(true);
            _travelCount--;
        }
    }
}
