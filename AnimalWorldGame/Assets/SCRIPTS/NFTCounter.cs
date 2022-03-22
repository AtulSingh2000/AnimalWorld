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

    public GameObject LemonTreeParent;
    public GameObject FigTreeParent;
    public GameObject OrangeTreeParent;
    public GameObject MangoTreeParent;
    public GameObject CoconutTreeParent;

    public int LemonTreeCount = 0;
    public int FigTreeCount = 0;
    public int OrangeTreeCount = 0;
    public int MangoTreeCount = 0;
    public int CoconutTreeCount = 0;
    public int CropFieldCount = 0;
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
    
     public void Start()
    {
       CheckTree1Count();
       CheckTree2Count();
       CheckTree3Count();
       CheckTree4Count();
       CheckTree5Count();
       CheckCropField();
       CheckMachines();

    
    }

    void Update()
    {
        SetTreeState();
    }

  

    public void CheckTree1Count()
    {
        for (int i = 1; i <= LemonTreeCount; i++)
        {
            
            Tree1[i-1].SetActive(true);
            
        }
    }
    public void CheckTree2Count()
    {
         for (int i = 1; i <= FigTreeCount; i++)
        {
            
            Tree2[i-1].SetActive(true);
            
        }
    }
    public void CheckTree3Count()
    {
         for (int i = 1; i <= OrangeTreeCount; i++)
        {
           
            Tree3[i-1].SetActive(true);
            
        }
    }
    public void CheckTree4Count()
    {
         for (int i = 1; i <= MangoTreeCount; i++)
        {
          
            Tree4[i-1].SetActive(true);
            
        }
    }
    public void CheckTree5Count()
    {
         for (int i = 1; i <= CoconutTreeCount; i++)
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

    public void SetTreeState()
    {
        if(LemonTreeCount == 0)
       {
           LemonTreeParent.SetActive(false);
       }
       else
       {
           LemonTreeParent.SetActive(true);
       }


       if(FigTreeCount == 0)
       {
           FigTreeParent.SetActive(false);
       }
       else
       {
           FigTreeParent.SetActive(true);
       }


       if(OrangeTreeCount == 0)
       {
           OrangeTreeParent.SetActive(false);
       }
       else{
           OrangeTreeParent.SetActive(true);
       }


       if(MangoTreeCount == 0)
       {
           MangoTreeParent.SetActive(false);
       }
       else{
           MangoTreeParent.SetActive(true);
       }

       if(CoconutTreeCount == 0)
       {
           CoconutTreeParent.SetActive(false);
       }
       else{
           CoconutTreeParent.SetActive(true);
       }
    }

}
