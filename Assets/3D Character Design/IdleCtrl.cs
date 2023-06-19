using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCtrl : StateMachineBehaviour
{//�̤W����ӬO����`�Ϊ��CEnter:�u�n�y�{�@�i�ӴN��������@���CUpdate:�|���_����CExit:�u�n�@���}�y�{�N����@���C
    float timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("xSpeed") > -0.1f && animator.GetFloat("xSpeed") < 0.1f && //����b���۪��ɭԨ��xSpeed�MzSpeed�٬O�|���ǷL�����ʡA
            animator.GetFloat("zSpeed") > -0.1f && animator.GetFloat("zSpeed") < 0.1f)
        { //�]���]�w�u�n�b�@�ӫܤp���d�򤺳���@�S�b�ʡC
            timer += Time.deltaTime;//���]�O�b60fps���q���A�o��N�O�[60�V���ɶ��A�]�N�|����1�����C
        }
        else timer = 0;//�p�ƾ��k�s�C

        if (timer > 5) {//���m�j��5��C
            timer = 0;//�p�ƾ��k�s�C
            animator.SetInteger("IdleType", Random.Range(0, 3));//�üƨM�wIdleType = 0~2�䤤�@�ӡCRandom.Range�g3�A�̤j�ȷ|��1�A����0~2�T��@�C
            animator.SetTrigger("IdlePlay");//�}�l���Ӱʵe�C
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
