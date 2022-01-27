using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{
   
   private Animator animator;
   private bool overworldCam = true;
   
   public CinemachineVirtualCamera OverworldCam;
   
   
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
               animator.Play("ThirdPerson");
           }
           
           else
           {
               animator.Play("Overworld");
               ZoomInOut();
           }
           overworldCam = !overworldCam;
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
