using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryShoot : MonoBehaviour
{
    //time 
    private float _totalTime=0;
    //gameobject
    public GameObject Bullet;
    public GameObject Gun;
    public GameObject pitch;
    public SentryCheckAttack Sentry;
    public Game GameOn;
    //flag
    public int ShootFlag,ShootCount;
    public bool AwakeFlag=false;
    public bool FailTag = false;
    public bool FailTagSentry = false;
    //platfrom move
    private Transform EnemyPos;
    //private Transform _dx, _dy;
    private Vector3 _direction,_directionX,_directionY,_lastPosition;
    private GameObject _enemy;
    //raycasting
    //public QueryTriggerInteraction queryTriggerInteraction= QueryTriggerInteraction.Ignore;
    
    void Start()
    {
        ShootFlag = 0;
        ShootCount = 0;
        GameOn = GameObject.Find("Game").GetComponent<Game>();
        Sentry=GameObject.Find("testRed").GetComponent<SentryCheckAttack>();
    }
    
    void Update()
    {
        FailTag = GameOn.EndTag;
        FailTagSentry = Sentry.FailTagSentry;
        //raycasting
        Ray ray = new Ray(Gun.transform.position, Gun.transform.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(Gun.transform.position, Gun.transform.position + Gun.transform.forward * 10, Color.yellow);
        
        //get enermy
        //if (Physics.Raycast(ray, out hitInfo,100,0,queryTriggerInteraction))
        if (Physics.Raycast(ray, out hitInfo))
        {
            EnemyPos=hitInfo.collider.gameObject.transform;
            //Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.name == "Car")
            {
                AwakeFlag = true;
                ShootFlag = 1;
            }
        }
        
        //platfrom move
        if (AwakeFlag && FailTagSentry==false && FailTag==false)
        {
            PlatformMove();
        }
        else if(AwakeFlag==false && FailTag==false && FailTagSentry==false)
        {
            pitch.transform.Rotate(Vector3.up,1.5f);
            Gun.transform.localRotation=Quaternion.Euler(20f,0,0);
        }
        
        //shoot
        _totalTime += Time.deltaTime;
        if (AwakeFlag && _totalTime > 2 && ShootFlag==1 && FailTag==false && FailTagSentry==false)
        {
            GameObject bullet=Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*7f);
            ShootFlag = 1;
            Destroy(bullet,10);
            ShootFlag = 0;
            _totalTime = 0;
            ShootCount += 1;
        }
        
    }

    void PlatformMove()
    {
        _direction = (EnemyPos.position-_lastPosition)+EnemyPos.position - transform.position;
        Debug.DrawRay(Gun.transform.position, _direction, Color.blue);
        _directionX = _direction;
        _directionX.y = 0;
        _directionY = _direction;
        Quaternion qx = Quaternion.LookRotation(_directionX);
        //Debug.Log(qx.eulerAngles);
        Quaternion qy = Quaternion.LookRotation(_directionY);
        pitch.transform.rotation = Quaternion.Slerp(pitch.transform.rotation, qx, 0.05f);
        Gun.transform.rotation = Quaternion.Slerp(Gun.transform.rotation, qy, 0.05f);

        _lastPosition = EnemyPos.position;
    }
}