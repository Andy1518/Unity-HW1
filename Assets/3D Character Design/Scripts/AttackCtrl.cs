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
        if (animator.GetBool("IsFighting")) {//���ˬd���S���bFighting���A�C
            if (Input.GetMouseButtonDown(0)) animator.SetTrigger("Attack");//�p�G�n�o��Attack���ȡA�n��GetBool�Ӥ��OGetTrigger�CTrigger�����O�]�OBool�C
        }
    }
}
