using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject P1;
    public GameObject P3;
    public bool EndTag;
    private bool camerastatus = true;
    void Start()
    {
        P1=GameObject.Find("P1Camera");
        P3=GameObject.Find("P3Camera");
        P1.SetActive(true);
        P3.SetActive(false);
        EndTag = GameObject.Find("Game").GetComponent<Game>().EndTag;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && EndTag==false)
        {
            if (camerastatus == false)
            {
                P1.SetActive(false);
                P3.SetActive(true);
                camerastatus = true;
            }
            else
            {
                P1.SetActive(true);
                P3.SetActive(false);
                camerastatus = false;
            }
        }
    }
}
