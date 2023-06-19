using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float damage = 180;//�w�]���Z���ˮ`��180�C
    Animator animator;
    private void Start()
    {
        animator = transform.root.GetComponent<Animator>();//�즹�Z�����ڪ���(�]�N�O�Ԥh)��Animator����ç�O�����m�s�banimator�C
    }
    private void OnTriggerEnter(Collider other){//����ƥ�GetComponent�A�]�����٤����D�|���֡A�ҥH�u��b�o�̧Y�ɧ��H����C
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")){//NameToLayer:��Layer�W�r�OEnemy���ন��Layer�s���X�ӡC��I�쪫��O�bEnemy�o�h�A�YĲ�o�C
            if(Mathf.Approximately(animator.GetFloat("AttackCurve"), 1)) {//��Ԥh����ʵe��Curve���ȵ���1�A�~�ǥX�ˮ`�P�w�T���C                
                //float�b�q���W�P�w�ܤ���T�A�ҥH�q�`�o�ɭԷ|�ϥ�Approximately�ӧP�w�ӭȬO�_�۪��1�A�p�G���N�|�����^��true�F�A�Ӥ��|�g==1�ӧP�w�C
            other.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);//SendMessageOptions.DontRequireReceiver:�q���C������Damage��k�A�����޹�H���W���S������k�C
            }//�p�G�ORequireReceiver�A�q�����o�{��H�S���o�Ӥ�k��AUnity�N�|�o�Ϳ��~�C
        }
    }
}
