using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove1 : MonoBehaviour
{
    public float YInput;
    public float YSet;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position
        YInput  = Input.GetAxis("Mouse Y");
        
        if(YInput!=0)
        {
            YSet += YInput  ;
            YSet = Mathf.Clamp(YSet, -10, 80);
            transform.localRotation = Quaternion.Euler(YSet, 0, 0);
        }
        /* Vector3 mousePosition = player.ScreenToWorldPoint(Input.mousePosition);
         transfrom.Rotate=*/
    }
}
