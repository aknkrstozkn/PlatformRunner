using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentController : CharacterController
{
    [SerializeField] GameObject player = null;
    private bool passedPlayer = false;

    void Start()
    {        
        Init();

        CalcPlacement();

        transform.gameObject.GetComponent<NavMeshAgent>().destination = targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.state == GameController.State.Race)
        {   
            CalcPlacement();
            if(Time.deltaTime == 1)
            {
              transform.gameObject.GetComponent<NavMeshAgent>().destination = targetPos;
            }            
            
        }
        else
        {   
            transform.gameObject.SetActive(false);
        }
        
    }

    void CalcPlacement()
    {
        if(transform.position.z > player.transform.position.z && !passedPlayer)
        {
            passedPlayer = true;
            GameController.placement += 1;
        }else if(transform.position.z < player.transform.position.z && passedPlayer)
        {
            passedPlayer = false;
            GameController.placement -= 1;
        }
    }
}
