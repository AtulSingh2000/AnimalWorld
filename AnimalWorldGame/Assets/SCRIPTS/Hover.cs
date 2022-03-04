using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hover : MonoBehaviour
{
    private CamSwitcher Cam;
    private bool state;
  
    // Start is called before the first frame update
    void Start()
    {
      Cam = FindObjectOfType<CamSwitcher>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(Cam.camState);
        }
    }

    private void OnMouseEnter()
    {
        if(Cam.camState == true)
        {
             foreach (Transform child in gameObject.transform) {
            child.gameObject.layer = LayerMask.NameToLayer("Highlighte");
        }
     
        }
        else
        {
            return;
        }
     

       // gameObject.layer = LayerMask.NameToLayer("Highlighte");
      
    }
    private void OnMouseExit()
    {
        // gameObject.layer = LayerMask.NameToLayer("Default");
         foreach (Transform child in gameObject.transform) 
         {
            child.gameObject.layer = LayerMask.NameToLayer("Default");;
         }
    }
}
