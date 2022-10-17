using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public Transform Camera;
    private float hor, ver;
    float x = 0, sc = 10;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position;
        hor = Input.GetAxis("Mouse X");
        ver = Input.GetAxis("Mouse Y");
        
        if(ver!=0)
        {
            x -= ver  ;
            x = Mathf.Clamp(x, -45, 45);
            transform.localRotation = Quaternion.Euler(x, 0, 0);
        }
        if (hor != 0)
        { Camera.Rotate(Camera.up, hor); }
        /* Vector3 mousePosition = player.ScreenToWorldPoint(Input.mousePosition);
         transfrom.Rotate=*/
    }
}
