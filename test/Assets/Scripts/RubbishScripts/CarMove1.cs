using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove1 : MonoBehaviour
{
    public float VerticalSpeed;
    public float HorizontalMoveSpeed;
    public float RawSpeed;
    public float V;
    public float H;
    public float M=0;
    //
    public WheelCollider LeftF;
    public WheelCollider LeftB;
    public WheelCollider RightF;
    public WheelCollider RightB;
    //
    public Transform ModelLeftF;
    public Transform ModelLeftB;
    public Transform ModelRightF;
    public Transform ModelRightB;
    //
    private float _speedLF, _speedLB, _speedRF, _speedRB;
    private  int _maxLinearSpeed=120;
    public float Speed1, Speed2, Speed3, Speed4;
    //PID
    
    public float Kp, Ki, Kd;
    public float Bias1=0, LastBias1=0;
    public float Bias2=0, LastBias2=0;
    public float Bias3=0, LastBias3=0;
    public float Bias4=0, LastBias4=0;
    public float Pwm1=0;
    public float Pwm2=0;
    public float Pwm3=0;
    public float Pwm4=0;

    public float Integral1, Integral2, Integral3, Integral4;
    // Start is called before the first frame update
    void Start()
    {
        //
        LeftF.steerAngle = -135;
        RightF.steerAngle = 135;
        LeftB.steerAngle = 135;
        RightB.steerAngle = -135;
        
    }

    // Update is called once per frame
    void Update()
    {
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
        M = Input.GetAxis("Mouse X");
        M = Mathf.Clamp(M, -1, 1);
        float xSpeed = H * HorizontalMoveSpeed;
        float ySpeed = V * VerticalSpeed ;
        float aSpeed = M * RawSpeed ;
        //
        Mecanum(xSpeed, ySpeed, aSpeed);
        //
        /*
        WheelModelUpdate(model_leftF, Pwm1);
        WheelModelUpdate(model_leftB, Pwm2);
        WheelModelUpdate(model_rightF, Pwm3);
        WheelModelUpdate(model_rightB, Pwm4);*/
    }
    void Mecanum(float xSpeed,float ySpeed,float aSpeed)
    {
        xSpeed = xSpeed * Mathf.Abs(xSpeed) / 80;
        ySpeed = ySpeed * Mathf.Abs(ySpeed) / 80;
        aSpeed = aSpeed * Mathf.Abs(aSpeed) / 100;
        Speed1 = (ySpeed + xSpeed + aSpeed);
        Speed2 = (ySpeed - xSpeed + aSpeed);
        Speed3 = (ySpeed + xSpeed - aSpeed);
        Speed4 = (ySpeed - xSpeed - aSpeed);
        
        SetMotor(Speed1, Speed2, Speed3, Speed4);
    }
    void SetMotor(float speed1,float speed2,float speed3,float speed4)
    {
        /*
        PID1(Pwm4 , speed4);
        PID2(Pwm3, speed3);
        PID3(Pwm1, speed1);
        PID4(Pwm2, speed2);
        */
        // print(leftF.motorTorque);
        //
        float max = Mathf.Abs(Pwm1);
        if (max < Mathf.Abs(Pwm2)) { max = Mathf.Abs(Pwm2); }
        if (max < Mathf.Abs(Pwm3)) { max = Mathf.Abs(Pwm3); }
        if (max < Mathf.Abs(Pwm4)) { max = Mathf.Abs(Pwm4); }

        if (max > VerticalSpeed)
        {
            Pwm1  = Pwm1  / max * VerticalSpeed;
            Pwm2  = Pwm2  / max * VerticalSpeed;
            Pwm3  = Pwm3 / max * VerticalSpeed;
            Pwm4  = Pwm4  / max * VerticalSpeed;
        }
        if (VerticalSpeed < Pwm1) { Pwm1= VerticalSpeed; }
        if (VerticalSpeed < Pwm2) { Pwm2= VerticalSpeed; }
        if (VerticalSpeed < Pwm3) { Pwm3= VerticalSpeed; }
        if (VerticalSpeed < Pwm4) { Pwm4= VerticalSpeed; }
        if (-VerticalSpeed > Pwm1) { Pwm1 = -VerticalSpeed; }
        if (-VerticalSpeed > Pwm2) { Pwm2 = -VerticalSpeed; }
        if (-VerticalSpeed > Pwm3) { Pwm3 = -VerticalSpeed; }
        if (-VerticalSpeed > Pwm4) { Pwm4 = -VerticalSpeed; }
        /*
        LeftF.motorTorque = Pwm1;
        LeftB.motorTorque = Pwm2;
        RightF.motorTorque = Pwm4;
        RightB.motorTorque = Pwm3;
        */
        LeftF.motorTorque = speed1 ;
        LeftB.motorTorque = speed2;
        RightB.motorTorque = speed3;
        RightF.motorTorque = speed4;
        
    }
    /*
    void PID1(float encoder,float target)
    {
        Bias1 = encoder - target;
        Integral1 += Bias1;
        print(target);
        Pwm1 -= Kp * Bias1  + Ki *Integral1 +Kd*(Bias1-LastBias1 );
        LastBias1 = Bias1; 
    }
    void PID2(float encoder, float target)
    {
        Bias2 = encoder - target;
        Integral2 += Bias2;
        Pwm2 -= Kp * Bias2 + Ki * Integral2 + Kd * (Bias2 - LastBias2);
        LastBias2 = Bias2;
    }
    void PID3(float encoder, float target)
    {
        Bias3 = encoder - target;
        Integral3 += Bias3;
        Pwm3 -= Kp * Bias3 + Ki *Integral3 + Kd * (Bias3 - LastBias3);
        LastBias3 = Bias3;
    }
    void PID4(float encoder, float target)
    {
        Bias4 = encoder - target;
        Integral4 += Bias4;
        Pwm4 -= Kp * Bias4 + Ki * Integral4 + Kd * (Bias4 - LastBias4);
        LastBias4 = Bias4;
    }
    
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
