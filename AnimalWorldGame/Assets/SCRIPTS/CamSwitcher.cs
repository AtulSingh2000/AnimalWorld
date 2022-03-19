using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{
   
   private Animator animator;
   public bool overworldCam = true;
   //private bool thirdPersonCam = true;
   
   public CinemachineVirtualCamera OverworldCam;
  

   public GameObject player;
   public GameObject playerOV;
   public bool camState;
   public bool isUIOpen = false;
   
    public float scrollSpeed = 10f;

    public float minFOV = 5f;
    public float maxFOV = 40f;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    
    
    void Start()
    {
        playerOV.SetActive(false);
        Cursor.visible = false;
               Cursor.lockState = CursorLockMode.Locked;
        // player.GetComponent<NavMeshAgent>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isUIOpen)
        {
            ZoomInOut();
        }
        
       if(Input.GetKeyDown(KeyCode.V))
       {
          // Debug.Log(camState);
           if(overworldCam)
           {
               camState = true;
               animator.Play("Overworld");
               playerOV.gameObject.transform.position = player.gameObject.transform.position;
               playerOV.SetActive(true);
               player.SetActive(false);
               OverworldCam.transform.position = new Vector3(51f, 51.25f, 78.6f);
               player.GetComponent<ThirdPersonMovement>().enabled = false;
               player.GetComponent<Animator>().enabled = false;
               //player.GetComponent<NavMeshAgent>().enabled = true;
               
               Cursor.visible = true;
               Cursor.lockState = CursorLockMode.None;
               
           }
           
           else
           {
               animator.Play("ThirdPerson");
               camState = false;
               
               player.gameObject.transform.position = playerOV.gameObject.transform.position;
               playerOV.SetActive(false);
               player.SetActive(true);
              // player.GetComponent<NavMeshAgent>().enabled = false;
               player.GetComponent<ThirdPersonMovement>().enabled = true;
               player.GetComponent<Animator>().enabled = true;
               Cursor.visible = false;
               Cursor.lockState = CursorLockMode.Locked;
              
               if(!isUIOpen)
               {
                   ZoomInOut();
               }

           }
           overworldCam = !overworldCam;
           //thirdPersonCam = !thirdPersonCam;
       }
    }

    public void OverworldCamera()
    {
        //cam.fieldOfView = 
    }
    public void ZoomInOut()
    {
      float FOV = OverworldCam.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
      FOV -=  Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

      FOV = Mathf.Clamp(FOV, minFOV, maxFOV);
      OverworldCam.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = FOV;
      //zoomCamera.transform.position = ClampCamera(zoomCamera.transform.position);
    }
}
