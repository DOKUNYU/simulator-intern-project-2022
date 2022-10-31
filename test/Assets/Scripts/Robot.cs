using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot
    // Start is called before the first frame update
{
    public float Blood;
    public bool RobotFailed = false;

    public float RobotBlood
    {
        get { return Blood; }
        set { Blood = value; }
    }

    public void ChangeBlood(float change)
    {
        if (Blood != 0)
        {
            Blood -= change;

        }
        else
        {
            RobotFailure();
        }

    }
    public void RobotFailure()
    {
        RobotFailed = true;
    }
}
