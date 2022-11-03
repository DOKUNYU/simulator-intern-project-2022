using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardCheckAttack : MonoBehaviour
{
    public int CountAttack = 0;
    public float StandardBlood,TotalBlood,BloodChange;
    public  bool FailTagStandard=false;
    public Robot myRobot = new Robot();
    
    public Image healthPoint;
    void Start()
    {
        
        myRobot.Blood = 500;
        StandardBlood = myRobot.RobotBlood;
        TotalBlood = StandardBlood;
    }

    // Update is called once per frame
    void Update()
    {
        StandardBlood = myRobot.RobotBlood;
        BloodChange = GetComponent<HeatGun>().BloodChange;
        if (BloodChange != 0)
        {
            myRobot.ChangeBlood(BloodChange);
        }
        ImageChange();
        if (StandardBlood == 0)
        {
            FailTagStandard = true;
        }
        //print(BaseBlood);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("bullet"))
        {
            CountAttack += 1;
            myRobot.ChangeBlood(100);
        }
        

    }
    void ImageChange()
    {
        healthPoint.fillAmount = StandardBlood / TotalBlood;
    }
    
}
