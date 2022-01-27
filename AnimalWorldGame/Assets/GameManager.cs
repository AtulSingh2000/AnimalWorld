using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject thirdPerson;
    public GameObject topDown;
    public GameObject player;
    public bool thirdP;
    public bool topD;
    // Start is called before the first frame update
    void Start()
    {
        thirdP = true;
        topD = false;
        topDown.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            if(thirdP)
            {
                thirdP = false;
                thirdPerson.SetActive(false);
                topD = true;
                topDown.SetActive(true);
                player.GetComponent<ThirdPersonMovement>().enabled = false;

            }
            else if(topD)
            {
                topD = false;
                thirdP = true;
                topDown.SetActive(false);
                thirdPerson.SetActive(true);
                player.GetComponent<ThirdPersonMovement>().enabled = true;
            }
        }

       
    }


}
