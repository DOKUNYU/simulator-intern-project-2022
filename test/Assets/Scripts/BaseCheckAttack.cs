using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseCheckAttack : MonoBehaviour
{
    public int CountAttack = 0;
    public float BaseBlood,TotalBlood;
    Base BlueBase = new Base();
    public Image healthPoint;
    public GameObject SentryRed;
    private bool _endTagSentry = false;
    void Start()
    {
        
        BlueBase.Blood = 2000;
        BaseBlood = BlueBase.BaseBlood;
        TotalBlood = BaseBlood;
        healthPoint.color=Color.blue;
        _endTagSentry = SentryRed.GetComponent<SentryCheckAttack>().FailTagSentry;
    }

    // Update is called once per frame
    void Update()
    {
        _endTagSentry = SentryRed.GetComponent<SentryCheckAttack>().FailTagSentry;
        if (_endTagSentry == true)
        {
            healthPoint.color=Color.white;
            BaseBlood = BlueBase.BaseBlood;
            BloodChange();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_endTagSentry == true)
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
