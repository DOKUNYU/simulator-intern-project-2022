using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float YInput;
    public float YSet,XSet;
    public float XInput=0;
    public float AngleDifference;
    public GameObject Car;
    public GameObject Platform;
    void Update()
    {
        Platform.transform.position = Car.transform.position;
        YInput  = Input.GetAxis("Mouse Y");
        XInput = Input.GetAxis("Mouse X");
        if(YInput!=0 || XInput!=0)
        {
            YSet += YInput;
            XSet += XInput;
            YSet = Mathf.Clamp(YSet, -10, 40);
            transform.localRotation = Quaternion.Euler(YSet, XSet, 0);
        }

        AngleDifference = transform.eulerAngles.y-Car.transform.eulerAngles.y;
        //this.transform.eulerAngles
        
        if (AngleDifference> 180 && AngleDifference < 360)
        {
            AngleDifference = AngleDifference - 360;
        }
        else if (AngleDifference > -360 && AngleDifference < -180)
        {
            AngleDifference = AngleDifference + 360;
        }

        if (AngleDifference < 5 && AngleDifference > -5)
        {
            AngleDifference = 0;
        }
        /* Vector3 mousePosition = player.ScreenToWorldPoint(Input.mousePosition);
         transfrom.Rotate=*/
    }
}