using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTCounter : MonoBehaviour
{
    public bool isBBQMachine;
    public bool isIceCreamMaker;
    public bool isFeederMachine;
    public bool isJuicer;
    public bool isPopcornMaker;
    public bool isMilkFactory;

    public GameObject BBQMachine;
    public GameObject IceCreamMaker;
    public GameObject FeederMachine;
    public GameObject Juicer;
    public GameObject PopcornMaker;
    public GameObject MilkFactory;
    
    public GameObject[] Tree1;
    public GameObject[] Tree2;
    public GameObject[] Tree3;
    public GameObject[] Tree4;
    public GameObject[] Tree5;

    public int Tree1Count;
    public int Tree2Count;
    public int Tree3Count;
    public int Tree4Count;
    public int Tree5Count;
    public int CropFieldCount;
    public GameObject[] CropField;

    

    
      void Awake()
    {   
         for (int i = 0; i<=4; i++)
       {
           Tree1[i].SetActive(false);
           Tree2[i].SetActive(false);
           Tree3[i].SetActive(false);
           Tree4[i].SetActive(false);
           Tree5[i].SetActive(false);
       }

       for (int i = 0; i<=2; i++)
       {
           CropField[i].SetActive(false);
       }

       BBQMachine.SetActive(false);
       IceCreamMaker.SetActive(false);
       FeederMachine.SetActive(false);

       Juicer.SetActive(false);

       MilkFactory.SetActive(false);

    }
    
     void Start()
    {
       CheckTree1Count();
       CheckTree2Count();
       CheckTree3Count();
       CheckTree4Count();
       CheckTree5Count();

       CheckCropField();

       CheckMachines();
    }

    // Update is called once per frame
     void Update()
    {
         
      
    }

    public void CheckTree1Count()
    {
        for (int i = 1; i <= Tree1Count; i++)
        {
            
            Tree1[i-1].SetActive(true);
            
        }
    }
    public void CheckTree2Count()
    {
         for (int i = 1; i <= Tree2Count; i++)
        {
            
            Tree2[i-1].SetActive(true);
            
        }
    }
    public void CheckTree3Count()
    {
         for (int i = 1; i <= Tree3Count; i++)
        {
           
            Tree3[i-1].SetActive(true);
            
        }
    }
    public void CheckTree4Count()
    {
         for (int i = 1; i <= Tree4Count; i++)
        {
          
            Tree4[i-1].SetActive(true);
            
        }
    }
    public void CheckTree5Count()
    {
         for (int i = 1; i <= Tree5Count; i++)
        {
            
            Tree5[i-1].SetActive(true);
            
        }
    }


    public void CheckCropField()
    {
        for (int i = 1; i<= CropFieldCount; i++)
        {
            CropField[i-1].SetActive(true);
        }
    }

    public void CheckMachines()
    {
        if(isBBQMachine)
        {
            BBQMachine.SetActive(true);
        }
         if(isFeederMachine)
        {
            FeederMachine.SetActive(true);
        }
         if(isIceCreamMaker)
        {
            IceCreamMaker.SetActive(true);
        }
         if(isJuicer)
        {
            Juicer.SetActive(true);
        }
         if(isMilkFactory)
        {
            MilkFactory.SetActive(true);
        }
    }

}
