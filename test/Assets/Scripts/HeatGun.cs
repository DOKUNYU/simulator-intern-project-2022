using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeatGun : MonoBehaviour
{
    //image
    public Image Heat;
    public Image HeatMax;
    //heat
    public float CurrHot,MaxHot;
    public float CoolingSpeed=0.025f;
    public int ShootFlag;
    //blood change
    Robot standard;
    public float TotalBlood; 
    //time 
    private float _totalTime=0;
    
    void Start()
    {
        CurrHot = 0;
        MaxHot = 50;
    }
    void Update()
    {
        
        _totalTime += Time.deltaTime;
        if (_totalTime > 0.1)
        {
            if (CurrHot > 0)
            {
               CoolingGun();
            }
            if (CurrHot >= MaxHot && CurrHot<2*MaxHot)
            {
                standard.ChangeBlood((CurrHot-MaxHot)/250/10*TotalBlood);
            }
            _totalTime = 0;
        }
        ShootFlag = GetComponent<Shoot>().ShootFlag;
        if (ShootFlag == 1)
        {
            CurrHot += 20;
        }
        if (CurrHot >= MaxHot)
        {
            HeatMax.fillAmount = 1;
            Heat.color=Color.red;
        }
        if (CurrHot < MaxHot)
        {
            HeatMax.fillAmount = 0;
            Heat.color=Color.white;
        }
        
        ImageChange();
    }

    void CoolingGun()
    {
        CurrHot = CurrHot - CoolingSpeed/10;
        CurrHot = (CurrHot < 0 ? 0 : CurrHot);
    }
    void ImageChange()
    {
        Heat.fillAmount = (CurrHot / MaxHot < 1 ? CurrHot / MaxHot : 1);
    }
}
