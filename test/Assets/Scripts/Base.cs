using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Base
{
    public float Blood;
    public bool BaseFailed = false;
    
    public float BaseBlood
    {
        get { return Blood; }
        set { Blood = value;
        }
    }

    public void ChangeBlood()
    {
        if (Blood != 0)
        {
            Blood -= 100;
            
        }
        else
        {
            Blood = 0;
        }
        
    }
}
