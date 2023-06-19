using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;//.main:����v�������Q�Хܬ�Main Camera�A���{���~�බ�Q����C
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = camTransform.position - transform.position;//��o��UI���V��v�����V�q�C
        Quaternion rot = Quaternion.LookRotation(dir);//��o���V�q�����ਤ�סC
        transform.rotation = new Quaternion(rot.x, rot.y, 0, rot.w);//��������FZ�b�H�~���s�����ਤ�סA���׬O�ѤW����o���C
    }
}
