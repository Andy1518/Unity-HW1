using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState : StateMachineBehaviour {
    enum ActionState { Idle, Walk, Close, LockMove, Attack, RunBack }
    //ActionState定義的是一個列舉的型別，可以不用再用其他程式定義。這邊是訂成私有的。
    AICharacterCtrl3D aiCtrl;
    Transform transform;//StateMachineBehaviour底下沒有訂transform這個功能可以直接點出來用，所以要自己定義。
    ActionState state;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!aiCtrl) aiCtrl = animator.GetComponent<AICharacterCtrl3D>();//為了獲得AI遊戲物件的程式。
        //這裡沒有GetComponent這個功能可以直接用，但只要可以獲得其中一個元件，就能由該元件反推得到其他元件。
        //所以這裡我們就能用Animator元件反推得得其它元件，抓到AICharacterCtrl3D這個程式。
        if (!transform) transform = animator.transform;//為了維持原本寫程式習慣，因此在這裡讓transform指向自己的transform。
        //OnStateEnter是只要流回這個狀態，就會執行一次。
        //因此前面加if(!aiCtrl)等，意思是當它們沒有指向記憶體位置時，就去抓記憶體位置。
        //表示已經有抓過該元件之後就不用再一直抓了，對效能多少有幫助。
    }

    float actEndTime = 0;//計數器，表示當前遊戲時間往後推多長的時間為動作完成時間，例如3秒。
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
     /* if (Time.time > actEndTime) {
             int rnd = Random.Range(0, 100);
             if(aiCtrl.lockTarget) { 當AI有鎖定目標時(在可見距離內)
                 float aiToPlayerDist = Vector3.Distance(transform.position, aiCtrl.lockTarget.position);
                 if(aiToPlayerDist < aiCtrl.atkRadius){ 當玩家在攻擊半徑內
                     if(aiToPlayerDist < aiCtrl.safeRadius) state = ActionState.RunBack; 當玩家在安全半徑內，往後跑                             
                     else state = ActionState.Attack; 當玩家在安全半徑外，攻擊
                 }
                 else { 當玩家在攻擊半徑外
                     if(rnd < 50) state = ActionState.LookAt; 注視
                     else state = ActionState.Close; 靠近
                 }
              }
              else { 當玩家沒有鎖定目標時(在可見距離外)
                  if (rnd < 50) state = ActionState.Idle;
                  else state = ActionState.Walk;
              }
        } 

         */
        if (Time.time > actEndTime) {//當計時時間到了，也就是動作時間結束以後
            int rnd = Random.Range(0, 100);//取亂數隨機決定狀態。取(0,100)實際上只會出現0~99，100不會出現。
            float toLockTargetDist = 0;
            if (aiCtrl.lockTarget) {//當有鎖定目標時(在可見距離內)
                toLockTargetDist = Vector3.Distance(transform.position, aiCtrl.lockTarget.position);//AI與玩家之間的距離。
                if (toLockTargetDist > aiCtrl.safeRadius) {//當玩家在AI安全半徑外，所以以下表示玩家在AI可見與安全範圍之間時採取的行動
                    if (aiCtrl.lockOn) {//在lockOn範圍內時
                        if (toLockTargetDist < aiCtrl.atkRadius) {//當玩家在AI攻擊半徑內
                            if (rnd < 50) state = ActionState.Attack;//攻擊
                            else state = ActionState.LockMove;//注視移動
                        }
                        else state = ActionState.LockMove;//當玩家在AI攻擊半徑外，注視移動
                    }
                    else {
                        state = ActionState.Close;//在lockOn範圍外時，靠近目標玩家
                    }
                }
                else
                    state = ActionState.RunBack;//當玩家在AI安全半徑內，向後跑
            }
            else { //沒有鎖定目標時就隨意走動。(在可見距離外)
                if (rnd < 50) //當亂數小於50就站著，當大於50就走動。
                    state = ActionState.Idle;
                else
                    state = ActionState.Walk;
            }
            
            switch (state) { //針對各個狀態設定
                case ActionState.Idle:
                    aiCtrl.axisInput = Vector2.zero;//移動速度為0。                
                    actEndTime = Time.time + 3;//以當前遊戲時間往後延三秒，表示這個Idle(閒置)動作時間為3秒。
                    break;
                case ActionState.Walk:
                    aiCtrl.axisInput = Vector2.up * 0.5f;//速度設定成0.5走路。
                    aiCtrl.destPosition = Quaternion.Euler(0, Random.Range(0, 360), 0) * transform.forward * 10 + transform.position;//四元數可以跟向量相乘得到一個向量。
                    actEndTime = Time.time + 3;//讓AI的Z軸方向向量乘上一個隨機的Y軸角度讓該向量旋轉，再乘上10表示要移動該結果單位向量之10倍距離，也就是10公尺，                                               
                    break;                     //再+transform.position做向量的平移，就能得到目的地座標。而最後只會讓AI往那個方向走3秒便會切換狀態。
                case ActionState.Close:
                    if (rnd < 75) aiCtrl.axisInput = Vector3.up * 0.5f;
                    else aiCtrl.axisInput = Vector3.zero;
                    actEndTime = Time.time + 1.5f;
                    break;
                case ActionState.LockMove:
                    if (rnd < 40) aiCtrl.axisInput = Vector2.left * 0.5f;//看著玩家往左走
                    else if (rnd < 80) aiCtrl.axisInput = Vector2.right * 0.5f;//看著玩家往右走                    
                    else {//20%機率
                        if(toLockTargetDist > aiCtrl.atkRadius)//當玩家在攻擊範圍外，
                            aiCtrl.axisInput = Vector2.up * 0.5f;//往前靠近玩家
                        else aiCtrl.axisInput = Vector2.zero;//當玩家進到攻擊範圍內，速度等於0，表示不動。
                    }
                    actEndTime = Time.time + 1.5f;//不要讓它停在這狀態太久，對測試時也不方便。
                    break;
                case ActionState.Attack:
                    aiCtrl.axisInput = Vector2.zero;//視情況決定攻擊時角色移動速度，此範例AI為法師，所以攻擊時速度為0，讓角色停在原地放招。
                    actEndTime = Time.time + 1.5f;
                    if (rnd < 20) animator.SetInteger("MagicType", 1);
                    else animator.SetInteger("MagicType", 0);
                    animator.SetTrigger("Attack");
                    break;
                case ActionState.RunBack://先拿AI座標減掉玩家座標並做平面投影後取單位向量，然後再把該向量半徑乘10以後做向量平移，
                    Vector3 backDirection = Vector3.ProjectOnPlane(transform.position - aiCtrl.lockTarget.position, Vector3.up).normalized;
                    Vector3 destPositon = backDirection * 10 + transform.position;//等於以AI為圓心往某個方向10公尺的座標為目的地。
                    if (NavMesh.SamplePosition(destPositon, out NavMeshHit hit, 9, -1)) {//為了避免AI往導航網格以外的目的地跑，這裡使用SamplePosition做檢測：
                        aiCtrl.destPosition = hit.position;//以目的地當作檢測點，然後檢查該點半徑9公尺內與導航網格重疊區域，計算出在網格內距離檢測點最近的位置座標，並填入hit。
                        aiCtrl.axisInput = Vector2.up;//再把hit設為目的地。檢查半徑不用10公尺是因為避免AI剛好就站在最邊邊，導致檢測完剛好AI站的位置就是計算結果，變成在原地不動。
                        actEndTime = Time.time + 2;//速度設定1，表示用跑的，Time設定跑2秒。
                    }
                    /*else { 如果上面檢測不到目標，可以在下面設定檢測不到的話要做什麼事，例如切換狀態成Attack。
                        state = ActionState.Attack;
                        aiCtrl.axisInput = Vector2.zero;
                        actEndTime = Time.time + 1.5f;
                    }*/
                    break;
            }
        }   
        else {//當AI動作狀態還沒結束時，持續執行以下程式：
          /*if(aiCtrl.lockTarget) {
                switch (state) {
                    case ActionState.Close:
                        aiCtrl.destPosition = aiCtrl.lockTarget.position;
                        break;
                    case ActionState.LockMove:
                    case ActionState.Attack:
                        ApplyRotation();
                        break;
                }*/
            if(aiCtrl.lockTarget) {
                switch (state) {
                    case ActionState.Close:
                    case ActionState.LockMove:
                    case ActionState.Attack:
                        aiCtrl.destPosition = aiCtrl.lockTarget.position;//以上三種狀態，目的地都是AI鎖定的玩家。
                        break;//因為玩家在AI動作結束前不可能都站在原地，所以寫在這裡表示在動作結束以前都會隨時偵測玩家位置並改變目的地座標。
                }
            }            
        }
    }
    /*
    void ApplyRotation() {
        Vector3 lookVector = (aiCtrl.lockTarget.position - transform.position).normalized;
        lookVector = transform.InverseTransformDirection(lookVector);
        float turnSpeed = Mathf.Lerp(180, 360, lookVector.z);
        float turnAmount = Mathf.Atan2(lookVector.x, lookVector.z);
        transform.Rotate(0, turnSpeed * turnAmount * Time.deltaTime, 0);
    }*/

}