using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class AIMovement : MonoBehaviour
{
    
    public NavMeshAgent playerNavMeshAgent;
    //public CinemachineVirtualCamera playerOverworldCam;
    public Camera playerCam;
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
            Ray myRay = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit myRaycastHit;

            if(Physics.Raycast(myRay, out myRaycastHit))
            {
                playerNavMeshAgent.SetDestination(myRaycastHit.point);
                
            }
        }
    }
}
