using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryMove : MonoBehaviour
{
    public bool RightTag = false;
    public bool FailTag = false;
    public bool EndTag = false;
    
    void Update()
    {
        FailTag = GetComponent<SentryCheckAttack>().FailTagSentry;
        EndTag = GameObject.Find("Game").GetComponent<Game>().EndTag;
        if (transform.localPosition.x >= -1.56f && transform.localPosition.x <= 0 && RightTag==false && FailTag==false && EndTag==false)
        {
            transform.Translate(Vector3.left * 0.002f);
        }
        if (transform.localPosition.x <= -1.5f && RightTag == false && FailTag==false && EndTag==false)
        {
            RightTag = true;
        }
        if (transform.localPosition.x >= -1.56f && transform.localPosition.x <= 0 && RightTag == true && FailTag==false && EndTag==false)
        {
            transform.Translate(Vector3.left * -0.002f);
            
        }
        if (transform.localPosition.x >= -0.06 && RightTag == true && FailTag==false&& EndTag==false)
        { RightTag = false;}
        //print(transform.localPosition.x);
    }
}
