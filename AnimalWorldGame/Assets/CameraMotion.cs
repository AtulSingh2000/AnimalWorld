using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    
    public float speed = 20f;
    public float scrollSpeed = 10f;
    private Vector3 dragOrigin;

    private Camera zoomCamera;
    // Start is called before the first frame update
    void Start()
    {
        zoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(zoomCamera.orthographic)
        {
            zoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        }



       Vector3 pos = transform.position;
       
       if(Input.GetKey("w"))
       {  
          pos.y += speed *Time.deltaTime;
          Mathf.Clamp(pos.y, 0f, 29f);
        }
        if(Input.GetKey("s"))
        {
            
            pos.y -= speed *Time.deltaTime;
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


        transform.position = pos;
    
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
            zoomCamera.transform.position += difference;
        }
    }

    
}
