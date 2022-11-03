using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //shoot
    public GameObject Bullet;
    public GameObject Gun;
    public int ShootFlag;
    //fail tag
    public bool EndTag = false;
    void Start()
    {
        ShootFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EndTag = GameObject.Find("Game").GetComponent<Game>().EndTag;
        ShootFlag = 0;
        if (Input.GetMouseButtonDown(0) && EndTag==false)
        {
            GameObject bullet=Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*7f);
            ShootFlag = 1;
            Destroy(bullet,10);
        }
    }
}
