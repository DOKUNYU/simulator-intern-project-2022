using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Gun;
    public int ShootFlag;
    void Start()
    {
        ShootFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ShootFlag = 0;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet=Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*7f);
            ShootFlag = 1;
            Destroy(bullet,10);
        }
    }
}
