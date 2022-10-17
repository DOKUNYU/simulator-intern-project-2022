using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float verticalSpeed;
    public float horizontalmoveSpeed;
    public float rawSpeed;
    public float v;
    public float h;
    public float m;
    //车轮碰撞器
    public WheelCollider leftF;
    public WheelCollider leftB;
    public WheelCollider rightF;
    public WheelCollider rightB;
    //车轮
    public Transform model_leftF;
    public Transform model_leftB;
    public Transform model_rightF;
    public Transform model_rightB;
    //速度
    private float speedLF, speedLB, speedRF, speedRB;
    private  int maxLinearSpeed=120;
    //PID
    public float kp, ki, kd;
    public float Bias1=0, Last_bias1=0;
    public float Bias2=0, Last_bias2=0;
    public float Bias3=0, Last_bias3=0;
    public float Bias4=0, Last_bias4=0;
    public float Pwm1=0;
    public float Pwm2=0;
    public float Pwm3=0;
    public float Pwm4=0;

    // Start is called before the first frame update
    void Start()
    {
        //设置成麦轮的角度
        leftF.steerAngle = 45;
        rightF.steerAngle = -45;
        leftB.steerAngle = -45;
        rightB.steerAngle = 45;
        
    }

    // Update is called once per frame
    void Update()
    {
        leftF.motorTorque = 0;
        leftB.motorTorque = 0;
        rightF.motorTorque = 0;
        rightB.motorTorque = 0;
        Pwm1 = 0;
        Pwm2 = 0;
        Pwm3 = 0;
        Pwm4 = 0;
        //roll pitch yaw
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        m = Input.GetAxis("Mouse X");
        float xSpeed = h * horizontalmoveSpeed;
        float ySpeed = v * verticalSpeed ;
        float aSpeed = m * rawSpeed ;
        //计算麦轮速度
        Mecanum(xSpeed, ySpeed, aSpeed);
        //更新模型
        WheelModel_Update(model_leftF, Pwm1);
        WheelModel_Update(model_leftB, Pwm2);
        WheelModel_Update(model_rightF, Pwm3);
        WheelModel_Update(model_rightB, Pwm4);
    }
    void Mecanum(float xSpeed,float ySpeed,float aSpeed)
    {
        xSpeed = xSpeed * Mathf.Abs(xSpeed) / 80;
        ySpeed = ySpeed * Mathf.Abs(ySpeed) / 80;
        aSpeed = aSpeed * Mathf.Abs(aSpeed) / 100;
        float speed1 = (ySpeed + xSpeed + aSpeed);
        float speed2 = (ySpeed - xSpeed + aSpeed);
        float speed3 = (ySpeed + xSpeed - aSpeed);
        float speed4 = (ySpeed - xSpeed - aSpeed);
        
        SetMotor(speed1, speed2, speed3, speed4);
    }
    void SetMotor(float speed1,float speed2,float speed3,float speed4)
    {
        PID1(Pwm4 , speed4);
        PID2(Pwm3, speed3);
        PID3(Pwm1, speed1);
        PID4(Pwm2, speed2);
        // print(leftF.motorTorque);
        //限制最大速度
        float max = Mathf.Abs(Pwm1);
        if (max < Mathf.Abs(Pwm2)) { max = Mathf.Abs(Pwm2); }
        if (max < Mathf.Abs(Pwm3)) { max = Mathf.Abs(Pwm3); }
        if (max < Mathf.Abs(Pwm4)) { max = Mathf.Abs(Pwm4); }

        if (max > maxLinearSpeed)
        {
            Pwm1  = Pwm1  / max * maxLinearSpeed;
            Pwm2  = Pwm2  / max * maxLinearSpeed;
            Pwm3  = Pwm3 / max * maxLinearSpeed;
            Pwm4  = Pwm4  / max * maxLinearSpeed;
        }/*
        if (maxLinearSpeed < Pwm1) { Pwm1= maxLinearSpeed; }
        if (maxLinearSpeed < Pwm2) { Pwm2= maxLinearSpeed; }
        if (maxLinearSpeed < Pwm3) { Pwm3= maxLinearSpeed; }
        if (maxLinearSpeed < Pwm4) { Pwm4= maxLinearSpeed; }
        if (-maxLinearSpeed > Pwm1) { Pwm1 = -maxLinearSpeed; }
        if (-maxLinearSpeed > Pwm2) { Pwm2 = -maxLinearSpeed; }
        if (-maxLinearSpeed > Pwm3) { Pwm3 = -maxLinearSpeed; }
        if (-maxLinearSpeed > Pwm4) { Pwm4 = -maxLinearSpeed; }*/

        leftF.motorTorque = Pwm1;
        leftB.motorTorque = Pwm2;
        rightF.motorTorque = Pwm4;
        rightB.motorTorque = Pwm3;
    }
    void PID1(float encoder,float target)
    {
        Bias1 = encoder - target;
        print(target);
        Pwm1 -= kp * Bias1  + ki * (Bias1+Last_bias1 )+kd*(Bias1-Last_bias1 );
        Last_bias1 = Bias1; 
    }
    void PID2(float encoder, float target)
    {
        Bias2 = encoder - target;
        Pwm2 -= kp * Bias2 + ki * (Bias2 + Last_bias2) + kd * (Bias2 - Last_bias2);
        Last_bias2 = Bias2;
    }
    void PID3(float encoder, float target)
    {
        Bias3 = encoder - target;
        Pwm3 -= kp * Bias3 + ki * (Bias3 + Last_bias3) + kd * (Bias3 - Last_bias3);
        Last_bias3 = Bias3;
    }
    void PID4(float encoder, float target)
    {
        Bias4 = encoder - target;
        Pwm4 -= kp * Bias4 + ki * (Bias4 + Last_bias4) + kd * (Bias4 - Last_bias4);
        Last_bias4 = Bias4;
    }
    void WheelModel_Update(Transform t,float pwm)
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
    }

}
