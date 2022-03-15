using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelection : MonoBehaviour
{
    //private Renderer renderer;
    public Camera camera;
    
    public GameObject truckPos;
    public GameObject truck;
    Ray ray;
    RaycastHit hit;
    private Transform _selection;

    
    // Start is called before the first frame update
    void Start()
    {
       // renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            var selectionLayer = _selection.gameObject.layer;
            selectionLayer = LayerMask.NameToLayer("Default");
            _selection = null;
        }

         ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit))
         {
             var selection = hit.transform;
             if(selection.CompareTag("NFT"))
             {
                 var selectionLayer = selection.gameObject.layer;
                // if(selectionLayer == LayerMask.NameToLayer("Default"))
                // {
                     selectionLayer = LayerMask.NameToLayer("Highlighte");
                // }
                 _selection = selection;
                 
             }
            
         }
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                
                var selection = hit.transform;
              
                if(selection.CompareTag("DMOBoard"))
                {
                    Instantiate(truck, truckPos.transform.position, Quaternion.identity);
                    StartCoroutine(TruckComeAndGo());
                    
                }
            }
        }
    }
    IEnumerator TruckComeAndGo()
    { 
        yield return new WaitForSeconds(7);
        
    }
   
}
