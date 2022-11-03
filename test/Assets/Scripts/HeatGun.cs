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
    public float CoolingSpeed=0.03f;
    public int ShootFlag;
    //blood change
    public float BloodChange = 0;
    public float TotalBlood = 0;
    private float _alpha = 0;
    //time 
    private float _totalTime=0;
    //flag
    private bool isPause = false;
    
    void Start()
    {
        CurrHot = 0;
        MaxHot = 50;
        HeatMax.fillAmount = 1;

    }
    void Update()
    {
        if (!isPause)
        {
            TotalBlood = GetComponent<StandardCheckAttack>().TotalBlood;
        }
        BloodChange = 0;
        _totalTime += Time.deltaTime;
        if (_totalTime > 0.1)
        {
            if (CurrHot > 0)
            {
               CoolingGun();
            }
            if (CurrHot >= MaxHot && CurrHot<2*MaxHot)
            {
                BloodChange=((CurrHot-MaxHot)/250/10*TotalBlood);
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
            _alpha = (CurrHot-MaxHot)/MaxHot;
            HeatMax.color = new Color(HeatMax.color.r, HeatMax.color.g, HeatMax.color.b, _alpha);
            Heat.color=Color.red;
        }
        if (CurrHot < MaxHot)
        {
            _alpha = 0;
            HeatMax.color = new Color(HeatMax.color.r, HeatMax.color.g, HeatMax.color.b, _alpha);
            Heat.color=Color.white;
        }

        if (CurrHot > 2 * MaxHot)
        {
            BloodChange = (CurrHot - 2 * MaxHot) / 250 * TotalBlood;
            CurrHot = 2 * MaxHot;
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
