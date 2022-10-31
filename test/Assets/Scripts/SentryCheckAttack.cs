using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentryCheckAttack : MonoBehaviour
{
    public int CountAttack = 0;
    public float SentryBlood,TotalBlood;
    public  bool FailTagSentry=false;
    Robot myRobot1 = new Robot();
    
    public Image healthPoint1;
    void Start()
    {
        
        myRobot1.Blood = 500;
        SentryBlood = myRobot1.RobotBlood;
        TotalBlood = SentryBlood;
    }

    // Update is called once per frame
    void Update()
    {
        SentryBlood = myRobot1.RobotBlood;
        ImageChange();
        if (SentryBlood == 0)
        {
            FailTagSentry = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        CountAttack += 1;
        myRobot1.ChangeBlood(100);

    }
    void ImageChange()
    {
        healthPoint1.fillAmount = SentryBlood / TotalBlood;
    }
}
