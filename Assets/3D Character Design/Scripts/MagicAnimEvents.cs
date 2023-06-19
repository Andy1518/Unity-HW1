using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAnimEvents : MonoBehaviour
{
    [SerializeField] GameObject fireballOnHand;
    AICharacterCtrl3D aiCtrl;
    private void Start(){
        aiCtrl = GetComponent<AICharacterCtrl3D>();
    }
    void CreateFireball() { //�`�N�W�٤@�w�n����e���]�w�ʵe�ƥ󪺦W�r�@�ˡC
        fireballOnHand.SetActive(true);//���W�����y�ɤl�t�ιw�m�����}��ܡC
    }
    void ShootFireball(GameObject fireballPrefab) {
        fireballOnHand.SetActive(false);//���W�����y�ɤl�t�ιw�m��������ܡC
        Instantiate(fireballPrefab, fireballOnHand.transform.position, //�ʺA�ͦ����y�w�m���A�q��W����y���y�Хͦ��A���y������V���V���a�C
            Quaternion.LookRotation(aiCtrl.lockTarget.position + Vector3.up * 1.3f - fireballOnHand.transform.position));
    }//LookRotation�̭��n��V�q�C��AICharacterCtrl3D�{���̭���lockTarget���o���a�y�СA�Ϊ��a�y�д�h���y�ͦ��y�СA�o��Ѥ��y���V���a���V�q�A
     //�A�ѸӦV�q�p����ਤ�ץX�ӡC�p�G�����ϥΪ��a�y�з|�ܦ����b���a�W���b���I�A
     //�]�����a�y�Эn�A���W�[�Ӥj��1.3���ءA�~�|�O���骺��m�A�]�N�O+ Vector3.up * 1.3f���ηN�C
    void CreateFireMeteor(GameObject fireMeteorPrefab) {
        Instantiate(fireMeteorPrefab, aiCtrl.lockTarget.position, Quaternion.identity);
    }//�ʺA�ͦ����k�ۡA��m�O���a��m�A���ݭn����C
}
