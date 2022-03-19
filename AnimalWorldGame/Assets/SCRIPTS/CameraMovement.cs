using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public float speed = 20f;
    public float scrollSpeed = 10f;

    public float minFOV = 5f;
    public float maxFOV = 40f;

    public MeshRenderer MapRenderer;
    private float mapMinX, mapMaxX, mapMinZ, mapMaxZ;

    private Vector3 dragOrigin;

    private Camera zoomCamera;
    public CamSwitcher cam_switch;

private void Awake()
{
    mapMinX = MapRenderer.transform.position.x - MapRenderer.bounds.size.x / 2f;
    mapMaxX = MapRenderer.transform.position.x + MapRenderer.bounds.size.x / 2f;

    mapMinZ = MapRenderer.transform.position.z - MapRenderer.bounds.size.z / 2f;
    mapMaxZ = MapRenderer.transform.position.z + MapRenderer.bounds.size.z / 2f;


}

    // Start is called before the first frame update
    void Start()
    {
        zoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cam_switch.isUIOpen)
        {
            PanCamera();
             Vector3 pos = transform.position;
       
       if(Input.GetKey("w"))
       {  
          pos.x += speed *Time.deltaTime;
          pos.z -= speed *Time.deltaTime;
          Mathf.Clamp(pos.y, 0f, 29f);
        }
        if(Input.GetKey("s"))
        {
            
            pos.x -= speed *Time.deltaTime;
          pos.z += speed *Time.deltaTime;
            Mathf.Clamp(pos.y, 0f, 29f);
            
        }
        if(Input.GetKey("a"))
        {
            pos.x += speed *Time.deltaTime;
            pos.z += speed *Time.deltaTime;
        }
        if(Input.GetKey("d"))
        {
            pos.x -= speed *Time.deltaTime;
            pos.z -= speed *Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -70f, 120f);
        pos.z = Mathf.Clamp(pos.z, 30f, 180f);
        transform.position = pos; 
        
        }

        

  
       // ZoomInOut();

      
    
    }

    public void PanCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = zoomCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - zoomCamera.ScreenToWorldPoint(Input.mousePosition);
            zoomCamera.transform.position = ClampCamera(zoomCamera.transform.position + difference);
            zoomCamera.transform.position += difference;
        }
    }

    public void ZoomInOut()
    {
      float FOV = zoomCamera.fieldOfView;
      FOV -=  Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

      FOV = Mathf.Clamp(FOV, minFOV, maxFOV);
      zoomCamera.fieldOfView = FOV;
      //zoomCamera.transform.position = ClampCamera(zoomCamera.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = zoomCamera.orthographicSize;
        float camWidth = zoomCamera.orthographicSize * zoomCamera.aspect;
        
        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minZ = mapMinZ + camHeight;
        float maxZ = mapMaxZ - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newZ = Mathf.Clamp(targetPosition.z, minZ, maxZ);
        //float newY = Mathf.Clamp(targetPosition.y, 55f, 130f);

        return new Vector3(newX, targetPosition.y, newZ);

    }


    
}
