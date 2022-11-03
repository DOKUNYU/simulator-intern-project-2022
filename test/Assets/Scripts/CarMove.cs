using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    //initial speed
    public float VerticalSpeed;
    public float HorizontalMoveSpeed;
    public float RawSpeed;
    public float V;
    public float H;
    public float M=0;
    //wheelCollider
    public WheelCollider LeftF;
    public WheelCollider LeftB;
    public WheelCollider RightF;
    public WheelCollider RightB;
    //Transform
    public Transform ModelLeftF;
    public Transform ModelLeftB;
    public Transform ModelRightF;
    public Transform ModelRightB;
    //set wheelspeed
    private float _speedLF, _speedLB, _speedRF, _speedRB;
    private  int _maxLinearSpeed=120;
    public float Speed1, Speed2, Speed3, Speed4;
    //PID
    public float Kp, Ki, Kd;
    public float Bias=0, LastBias=0;
    public float Pwm = 0;
    public float Pwm1=0;
    public float Pwm2=0;
    public float Pwm3=0;
    public float Pwm4=0;
    public float Integral;
    //get z speed
    public GameObject Platform;
    public PlatformMove PlatformMove;
    //fail tag
    public bool EndTag = false;
    void Start()
    {
        //set mecanum wheel angle
        LeftF.steerAngle = -135;
        RightF.steerAngle = 135;
        LeftB.steerAngle = 135;
        RightB.steerAngle = -135;
        //Get wheel component
        

    }

    // Update is called once per frame
    void Update()
    {
        //get component
        PlatformMove = Platform.GetComponent<PlatformMove>();
        EndTag = GetComponent<StandardCheckAttack>().FailTagStandard;
        //set initial value
        LeftF.motorTorque = 0;
        LeftB.motorTorque = 0;
        RightF.motorTorque = 0;
        RightB.motorTorque = 0;
        Pwm1 = 0;
        Pwm2 = 0;
        Pwm3 = 0;
        Pwm4 = 0;
        //roll pitch yaw
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");
        M = PlatformMove.AngleDifference;
        float xSpeed = H * HorizontalMoveSpeed;
        float ySpeed = V * VerticalSpeed ;
        if (EndTag == false)
        {
            Mecanum(xSpeed, ySpeed, M);
        }
        
        //
        /*
        WheelModelUpdate(model_leftF, Pwm1);
        WheelModelUpdate(model_leftB, Pwm2);
        WheelModelUpdate(model_rightF, Pwm3);
        WheelModelUpdate(model_rightB, Pwm4);*/
        

    }
    void Mecanum(float xSpeed,float ySpeed,float aSpeed)
    {
        PID(aSpeed);
        xSpeed = Mathf.Clamp(xSpeed, -30, 30);
        ySpeed = Mathf.Clamp(ySpeed, -30, 30);
        aSpeed = Mathf.Clamp(Pwm, -30, 30);
        Speed1 = (ySpeed + xSpeed + aSpeed);
        Speed2 = (ySpeed - xSpeed + aSpeed);
        Speed3 = (ySpeed + xSpeed - aSpeed);
        Speed4 = (ySpeed - xSpeed - aSpeed);
        
        SetMotor(Speed1, Speed2, Speed3, Speed4);
    }
    void SetMotor(float speed1,float speed2,float speed3,float speed4)
    {
        
        /*// print(leftF.motorTorque);
        Pwm1= Mathf.Clamp(Pwm1, -_maxLinearSpeed, _maxLinearSpeed);
        Pwm2= Mathf.Clamp(Pwm2, -_maxLinearSpeed, _maxLinearSpeed);
        Pwm3= Mathf.Clamp(Pwm3, -_maxLinearSpeed, _maxLinearSpeed);
        Pwm4= Mathf.Clamp(Pwm4, -_maxLinearSpeed, _maxLinearSpeed);
        float max = Mathf.Abs(Pwm1);
        if (max < Mathf.Abs(Pwm2)) { max = Mathf.Abs(Pwm2); }
        if (max < Mathf.Abs(Pwm3)) { max = Mathf.Abs(Pwm3); }
        if (max < Mathf.Abs(Pwm4)) { max = Mathf.Abs(Pwm4); }

        if (max > _maxLinearSpeed)
        {
            Pwm1  = Pwm1  / max * _maxLinearSpeed;
            Pwm2  = Pwm2  / max * _maxLinearSpeed;
            Pwm3  = Pwm3 / max * _maxLinearSpeed;
            Pwm4  = Pwm4  / max * _maxLinearSpeed;
        }*/
        Pwm1= Mathf.Clamp(speed1, -_maxLinearSpeed, _maxLinearSpeed);
        Pwm2= Mathf.Clamp(speed2, -_maxLinearSpeed, _maxLinearSpeed);
        Pwm3= Mathf.Clamp(speed3, -_maxLinearSpeed, _maxLinearSpeed);
        Pwm4= Mathf.Clamp(speed4, -_maxLinearSpeed, _maxLinearSpeed);
        
        LeftF.motorTorque = Pwm1;
        LeftB.motorTorque = Pwm2;
        RightF.motorTorque = Pwm4;
        RightB.motorTorque = Pwm3;
        /*
        LeftF.motorTorque = speed1 ;
        LeftB.motorTorque = speed2;
        RightB.motorTorque = speed3;
        RightF.motorTorque = speed4;
        */
    }
    
    void PID(float bias)
    {
        Bias = bias;
        Integral += Bias*0.00000005f;
        Pwm+= Kp * Bias  + Ki *Integral +Kd*(Bias-LastBias);
        LastBias = Bias; 
    }
    
    /*
    void WheelModelUpdate(Transform t,float pwm)
    {
        //Vector3 pos = t.position;
        //Quaternion rot = t.rotation;
        //Vector3 relativePos = new Vector3(90, 0, 0);
        //wheel.GetWorldPose(out pos, out rot);
        //rot = Quaternion.Euler(0,90,0);
        //print(rot);
        //t.position = pos;
        //t.rotation = rot;
        //t.transform.Rotate(Vector3.forward, pwm);
    }*/
    
}
