using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;//.main:該攝影機必須被標示為Main Camera，此程式才能順利執行。
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = camTransform.position - transform.position;//獲得由UI指向攝影機的向量。
        Quaternion rot = Quaternion.LookRotation(dir);//獲得此向量的旋轉角度。
        transform.rotation = new Quaternion(rot.x, rot.y, 0, rot.w);//給血條除了Z軸以外的新的旋轉角度，角度是由上面獲得的。
    }
}
