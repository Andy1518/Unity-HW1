using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCtrl : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsFighting")) {//先檢查有沒有在Fighting狀態。
            if (Input.GetMouseButtonDown(0)) animator.SetTrigger("Attack");//如果要得到Attack的值，要用GetBool而不是GetTrigger。Trigger的型別也是Bool。
        }
    }
}
