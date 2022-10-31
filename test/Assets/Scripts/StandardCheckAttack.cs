using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardCheckAttack : MonoBehaviour
{
    public int CountAttack = 0;
    public float StandardBlood,TotalBlood;
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
        ImageChange();
        //print(BaseBlood);
    }

    void OnCollisionEnter(Collision collision)
    {
        CountAttack += 1;
        myRobot.ChangeBlood(100);

    }
    void ImageChange()
    {
        healthPoint.fillAmount = StandardBlood / TotalBlood;
    }
}
