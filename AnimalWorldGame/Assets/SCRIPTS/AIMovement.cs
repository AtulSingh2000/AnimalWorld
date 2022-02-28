using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.EventSystems;

public class AIMovement : MonoBehaviour
{
    
    public NavMeshAgent playerNavMeshAgent;
    //public CinemachineVirtualCamera playerOverworldCam;
    public Camera playerCam;
    public Animator playerAnim;
    //public bool isRunning;

    //public Animation run;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            return;
            
            Ray myRay = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if(Physics.Raycast(myRay, out myRaycastHit))
            {
                playerNavMeshAgent.SetDestination(myRaycastHit.point);
                
            }
        }

       if(playerNavMeshAgent.remainingDistance <= playerNavMeshAgent.stoppingDistance)
        {
            //isRunning = false;
             
             playerAnim.SetBool("Run Axe", false);
        }
        else
        {
            //isRunning = true;
            playerAnim.SetBool("Run Axe", true);
        }

       // playerAnim.SetBool("Run Axe", isRunning);
    
    }
}
