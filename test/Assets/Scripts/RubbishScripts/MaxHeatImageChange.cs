using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxHeatImageChange : MonoBehaviour
{
    //图片原大小(压缩前的)
    public Vector2 textureOriginSize = new Vector2(1000, 1000);
    // Start is called before the first frame update
    void Start()
    {
        Scaler();
    }

    //适配
    void Scaler()
    {
        //当前画布尺寸
        Vector2 canvasSize = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
        //当前画布尺寸长宽比
        float screenxyRate = canvasSize.x / canvasSize.y;

        //RectTransform.Width
    }

    public void Update()
    {

    }

}