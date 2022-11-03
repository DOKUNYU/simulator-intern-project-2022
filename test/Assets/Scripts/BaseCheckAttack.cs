using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseCheckAttack : MonoBehaviour
{
    //flag
    public int CountAttack = 0;
    public float BaseBlood,TotalBlood;
    public Base BlueBase = new Base();
    public Image healthPoint;
    public GameObject SentryRed;
    public bool EndTagSentry = false;
    public bool FailTagBase = false;
    void Start()
    {
        BlueBase.Blood = 2000;
        Initial();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        EndTagSentry = SentryRed.GetComponent<SentryCheckAttack>().FailTagSentry;
        if (EndTagSentry == true && FailTagBase==false)
        {
            healthPoint.color=Color.white;
            BaseBlood = BlueBase.BaseBlood;
            Debug.Log(BaseBlood);
            if (BaseBlood == 0)
            {
                FailTagBase = true;
            }
            else
            {
                BloodChange();
            }
            
        }
    }

    public void Initial()
    {
        BaseBlood = BlueBase.BaseBlood;
        TotalBlood = BaseBlood;
        BloodChange();
        healthPoint.color=Color.blue;
        EndTagSentry = SentryRed.GetComponent<SentryCheckAttack>().FailTagSentry;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (EndTagSentry == true && FailTagBase== false)
        {
            CountAttack += 1;
            BlueBase.ChangeBlood();
        }
    }
    void BloodChange()
    {
        healthPoint.fillAmount = BaseBlood / TotalBlood;
    }
    
}
