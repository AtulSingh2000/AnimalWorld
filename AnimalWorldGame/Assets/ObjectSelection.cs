using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    //private Renderer renderer;
    public Camera camera;
    public GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
       // renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
