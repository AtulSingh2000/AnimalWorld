using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{
   
   private Animator animator;
   private bool overworldCam = true;
   //private bool thirdPersonCam = true;
   
   public CinemachineVirtualCamera OverworldCam;
  

   public GameObject player;
   
   
    public float scrollSpeed = 10f;

    public float minFOV = 5f;
    public float maxFOV = 40f;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ZoomInOut();
       if(Input.GetKeyDown(KeyCode.V))
       {
           if(overworldCam)
           {
               animator.Play("Overworld");
               OverworldCam.transform.position = new Vector3(51f, 51.25f, 78.6f);
               player.GetComponent<ThirdPersonMovement>().enabled = false;
               player.GetComponent<Animator>().enabled = false;
               
               Cursor.visible = true;
               
           }
           
           else
           {
               animator.Play("ThirdPerson");
               ZoomInOut();
               player.GetComponent<ThirdPersonMovement>().enabled = true;
               player.GetComponent<Animator>().enabled = true;
               Cursor.visible = false;

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
