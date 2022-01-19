using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public float speed = 20f;
    public float scrollSpeed = 10f;

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


        transform.position = pos;
    
    }

    
}
