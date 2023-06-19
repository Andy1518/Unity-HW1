using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCtrl : StateMachineBehaviour
{//最上面兩個是比較常用的。Enter:只要流程一進來就首先執行一次。Update:會不斷執行。Exit:只要一離開流程就執行一次。
    float timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("xSpeed") > -0.1f && animator.GetFloat("xSpeed") < 0.1f && //角色在停著的時候其實xSpeed和zSpeed還是會有些微的移動，
            animator.GetFloat("zSpeed") > -0.1f && animator.GetFloat("zSpeed") < 0.1f)
        { //因此設定只要在一個很小的範圍內都當作沒在動。
            timer += Time.deltaTime;//假設是在60fps的電腦，這邊就是加60幀的時間，也就會等於1秒鐘。
        }
        else timer = 0;//計數器歸零。

        if (timer > 5) {//當閒置大於5秒。
            timer = 0;//計數器歸零。
            animator.SetInteger("IdleType", Random.Range(0, 3));//亂數決定IdleType = 0~2其中一個。Random.Range寫3，最大值會減1，等於0~2三選一。
            animator.SetTrigger("IdlePlay");//開始播該動畫。
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
