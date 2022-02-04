using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
       // gameObject.layer = LayerMask.NameToLayer("Highlighte");
        foreach (Transform child in gameObject.transform) {
            child.gameObject.layer = LayerMask.NameToLayer("Highlighte");;
        }
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
