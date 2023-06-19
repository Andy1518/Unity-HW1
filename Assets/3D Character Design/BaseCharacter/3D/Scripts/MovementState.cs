using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState : StateMachineBehaviour {
    enum ActionState { Idle, Walk, Close, LockMove, Attack, RunBack }
    //ActionState�w�q���O�@�ӦC�|�����O�A�i�H���ΦA�Ψ�L�{���w�q�C�o��O�q���p�����C
    AICharacterCtrl3D aiCtrl;
    Transform transform;//StateMachineBehaviour���U�S���qtransform�o�ӥ\��i�H�����I�X�ӥΡA�ҥH�n�ۤv�w�q�C
    ActionState state;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!aiCtrl) aiCtrl = animator.GetComponent<AICharacterCtrl3D>();//���F��oAI�C�����󪺵{���C
        //�o�̨S��GetComponent�o�ӥ\��i�H�����ΡA���u�n�i�H��o�䤤�@�Ӥ���A�N��ѸӤ���ϱ��o���L����C
        //�ҥH�o�̧ڭ̴N���Animator����ϱ��o�o�䥦����A���AICharacterCtrl3D�o�ӵ{���C
        if (!transform) transform = animator.transform;//���F�����쥻�g�{���ߺD�A�]���b�o����transform���V�ۤv��transform�C
        //OnStateEnter�O�u�n�y�^�o�Ӫ��A�A�N�|����@���C
        //�]���e���[if(!aiCtrl)���A�N��O���̨S�����V�O�����m�ɡA�N�h��O�����m�C
        //��ܤw�g����L�Ӥ��󤧫�N���ΦA�@����F�A��į�h�֦����U�C
    }

    float actEndTime = 0;//�p�ƾ��A��ܷ�e�C���ɶ�������h�����ɶ����ʧ@�����ɶ��A�Ҧp3��C
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
     /* if (Time.time > actEndTime) {
             int rnd = Random.Range(0, 100);
             if(aiCtrl.lockTarget) { ��AI����w�ؼЮ�(�b�i���Z����)
                 float aiToPlayerDist = Vector3.Distance(transform.position, aiCtrl.lockTarget.position);
                 if(aiToPlayerDist < aiCtrl.atkRadius){ ���a�b�����b�|��
                     if(aiToPlayerDist < aiCtrl.safeRadius) state = ActionState.RunBack; ���a�b�w���b�|���A����]                             
                     else state = ActionState.Attack; ���a�b�w���b�|�~�A����
                 }
                 else { ���a�b�����b�|�~
                     if(rnd < 50) state = ActionState.LookAt; �`��
                     else state = ActionState.Close; �a��
                 }
              }
              else { ���a�S����w�ؼЮ�(�b�i���Z���~)
                  if (rnd < 50) state = ActionState.Idle;
                  else state = ActionState.Walk;
              }
        } 

         */
        if (Time.time > actEndTime) {//��p�ɮɶ���F�A�]�N�O�ʧ@�ɶ������H��
            int rnd = Random.Range(0, 100);//���ü��H���M�w���A�C��(0,100)��ڤW�u�|�X�{0~99�A100���|�X�{�C
            float toLockTargetDist = 0;
            if (aiCtrl.lockTarget) {//����w�ؼЮ�(�b�i���Z����)
                toLockTargetDist = Vector3.Distance(transform.position, aiCtrl.lockTarget.position);//AI�P���a�������Z���C
                if (toLockTargetDist > aiCtrl.safeRadius) {//���a�bAI�w���b�|�~�A�ҥH�H�U��ܪ��a�bAI�i���P�w���d�򤧶��ɱĨ������
                    if (aiCtrl.lockOn) {//�blockOn�d�򤺮�
                        if (toLockTargetDist < aiCtrl.atkRadius) {//���a�bAI�����b�|��
                            if (rnd < 50) state = ActionState.Attack;//����
                            else state = ActionState.LockMove;//�`������
                        }
                        else state = ActionState.LockMove;//���a�bAI�����b�|�~�A�`������
                    }
                    else {
                        state = ActionState.Close;//�blockOn�d��~�ɡA�a��ؼЪ��a
                    }
                }
                else
                    state = ActionState.RunBack;//���a�bAI�w���b�|���A�V��]
            }
            else { //�S����w�ؼЮɴN�H�N���ʡC(�b�i���Z���~)
                if (rnd < 50) //��üƤp��50�N���ۡA��j��50�N���ʡC
                    state = ActionState.Idle;
                else
                    state = ActionState.Walk;
            }
            
            switch (state) { //�w��U�Ӫ��A�]�w
                case ActionState.Idle:
                    aiCtrl.axisInput = Vector2.zero;//���ʳt�׬�0�C                
                    actEndTime = Time.time + 3;//�H��e�C���ɶ����᩵�T��A��ܳo��Idle(���m)�ʧ@�ɶ���3��C
                    break;
                case ActionState.Walk:
                    aiCtrl.axisInput = Vector2.up * 0.5f;//�t�׳]�w��0.5�����C
                    aiCtrl.destPosition = Quaternion.Euler(0, Random.Range(0, 360), 0) * transform.forward * 10 + transform.position;//�|���ƥi�H��V�q�ۭ��o��@�ӦV�q�C
                    actEndTime = Time.time + 3;//��AI��Z�b��V�V�q���W�@���H����Y�b�������ӦV�q����A�A���W10��ܭn���ʸӵ��G���V�q��10���Z���A�]�N�O10���ءA                                               
                    break;                     //�A+transform.position���V�q�������A�N��o��ت��a�y�СC�ӳ̫�u�|��AI�����Ӥ�V��3��K�|�������A�C
                case ActionState.Close:
                    if (rnd < 75) aiCtrl.axisInput = Vector3.up * 0.5f;
                    else aiCtrl.axisInput = Vector3.zero;
                    actEndTime = Time.time + 1.5f;
                    break;
                case ActionState.LockMove:
                    if (rnd < 40) aiCtrl.axisInput = Vector2.left * 0.5f;//�ݵ۪��a������
                    else if (rnd < 80) aiCtrl.axisInput = Vector2.right * 0.5f;//�ݵ۪��a���k��                    
                    else {//20%���v
                        if(toLockTargetDist > aiCtrl.atkRadius)//���a�b�����d��~�A
                            aiCtrl.axisInput = Vector2.up * 0.5f;//���e�a�񪱮a
                        else aiCtrl.axisInput = Vector2.zero;//���a�i������d�򤺡A�t�׵���0�A��ܤ��ʡC
                    }
                    actEndTime = Time.time + 1.5f;//���n�������b�o���A�Ӥ[�A����ծɤ]����K�C
                    break;
                case ActionState.Attack:
                    aiCtrl.axisInput = Vector2.zero;//�����p�M�w�����ɨ��Ⲿ�ʳt�סA���d��AI���k�v�A�ҥH�����ɳt�׬�0�A�����ⰱ�b��a��ۡC
                    actEndTime = Time.time + 1.5f;
                    if (rnd < 20) animator.SetInteger("MagicType", 1);
                    else animator.SetInteger("MagicType", 0);
                    animator.SetTrigger("Attack");
                    break;
                case ActionState.RunBack://����AI�y�д���a�y�Шð�������v������V�q�A�M��A��ӦV�q�b�|��10�H�ᰵ�V�q�����A
                    Vector3 backDirection = Vector3.ProjectOnPlane(transform.position - aiCtrl.lockTarget.position, Vector3.up).normalized;
                    Vector3 destPositon = backDirection * 10 + transform.position;//����HAI����ߩ��Y�Ӥ�V10���ت��y�Ь��ت��a�C
                    if (NavMesh.SamplePosition(destPositon, out NavMeshHit hit, 9, -1)) {//���F�קKAI���ɯ����H�~���ت��a�]�A�o�̨ϥ�SamplePosition���˴��G
                        aiCtrl.destPosition = hit.position;//�H�ت��a��@�˴��I�A�M���ˬd���I�b�|9���ؤ��P�ɯ���歫�|�ϰ�A�p��X�b���椺�Z���˴��I�̪񪺦�m�y�СA�ö�Jhit�C
                        aiCtrl.axisInput = Vector2.up;//�A��hit�]���ت��a�C�ˬd�b�|����10���جO�]���קKAI��n�N���b������A�ɭP�˴�����nAI������m�N�O�p�⵲�G�A�ܦ��b��a���ʡC
                        actEndTime = Time.time + 2;//�t�׳]�w1�A��ܥζ]���ATime�]�w�]2��C
                    }
                    /*else { �p�G�W���˴�����ؼСA�i�H�b�U���]�w�˴����쪺�ܭn������ơA�Ҧp�������A��Attack�C
                        state = ActionState.Attack;
                        aiCtrl.axisInput = Vector2.zero;
                        actEndTime = Time.time + 1.5f;
                    }*/
                    break;
            }
        }   
        else {//��AI�ʧ@���A�٨S�����ɡA�������H�U�{���G
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
                        aiCtrl.destPosition = aiCtrl.lockTarget.position;//�H�W�T�ت��A�A�ت��a���OAI��w�����a�C
                        break;//�]�����a�bAI�ʧ@�����e���i�ೣ���b��a�A�ҥH�g�b�o�̪�ܦb�ʧ@�����H�e���|�H�ɰ������a��m�ç��ܥت��a�y�СC
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