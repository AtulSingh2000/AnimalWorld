using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelection : MonoBehaviour
{
    //private Renderer renderer;
    public Camera camera;
    public GameObject panel;
    Ray ray;
    RaycastHit hit;

    
    // Start is called before the first frame update
    void Start()
    {
       // renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
         ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit))
         {
             if(hit.collider.gameObject.CompareTag("NFT"))
             {
                 hit.collider.gameObject.layer = LayerMask.NameToLayer("Highlighte");
             }
         }
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfor))
            {
                if(hitInfor.collider.gameObject.CompareTag("NFT"))
                {
                    
                    panel.SetActive(true);
                }
            }
        }
    }
   
}
