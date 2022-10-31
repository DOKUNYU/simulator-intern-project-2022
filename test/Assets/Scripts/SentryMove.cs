using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryMove : MonoBehaviour
{
    public bool RightTag = false;
    public bool EndTag = false;
    void Update()
    {
        EndTag = GetComponent<SentryCheckAttack>().FailTagSentry;
        if (transform.localPosition.x >= -1.56f && transform.localPosition.x <= 0 && RightTag==false && EndTag==false)
        {
            transform.Translate(Vector3.left * 0.001f);
        }
        if (transform.localPosition.x <= -1.5f && RightTag == false)
        {
            RightTag = true;
        }
        if (transform.localPosition.x >= -1.56f && transform.localPosition.x <= 0 && RightTag == true && EndTag==false)
        {
            transform.Translate(Vector3.left * -0.001f);
            
        }
        if (transform.localPosition.x >= -0.06 && RightTag == true && EndTag==false)
        { RightTag = false;}
        //print(transform.localPosition.x);
    }
}
