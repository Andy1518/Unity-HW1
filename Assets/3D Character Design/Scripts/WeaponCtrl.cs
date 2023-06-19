using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform weaponHolderOnBack, weaponHolderOnHand;
    [SerializeField] Transform[] attackHolders;
    [SerializeField] GameObject[] weaponTrails;

    Animator animator;
    bool isFighting = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {//����LTab��C
            //isFighting = !isFighting;�̷ӷ�e���A���ۤϭ��мg�^�h�C
            //animator.SetBool("IsFighting", isFighting);��ȼg�i�hIsFighting�C
            //�H�W���t�X��ʵe�y�{�]�p���{���g�k�A�Ӥ�k�|�ɭP����b�����Z���ɾ�ӤH���U�Ӷ]�ʵe�C�H�U���s���k�G
            if (isFighting) {//��IsFighting��true�C
                animator.SetTrigger("PutWeapon");//���b�������A�AĲ�o���Z���ʵe�C
            }
            else {//��IsFighting��false�C
                animator.SetTrigger("GetWeapon");//�S�b�������A�AĲ�o���Z���ʵe�C
            }
        }
    }
    void GetWeapon() {
        isFighting = true;
        animator.SetBool("IsFighting", true);
        weapon.SetParent(weaponHolderOnHand);//��Weapon��������令WeaponHolderOnHand���V��WeaponHolder�A�N�O�e���ƥ��즲�n����W��WeaponHolder�ťժ���C
        weapon.localPosition = Vector3.zero;//�N�Z�����y���k�s�A����Reset�C
        weapon.localRotation = Quaternion.identity;//�N�Z�������ਤ���k�s�A����Reset�C
    }
    void PutWeapon() {
        isFighting = false;
        animator.SetBool("IsFighting", false);
        weapon.SetParent(weaponHolderOnBack);//��Weapon��������令WeaponHolderOnBack���V��WeaponHolder�A�N�O�e���ƥ��즲�n���I�W��WeaponHolder�ťժ���C
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.identity;
    }
    void WeaponTrailEffect(int index) {
        Instantiate(weaponTrails[index], attackHolders[index]);//�ʺA�ͦ��C��weaponTrails�̧ǰʺA�ͦ���attackHolders�C
    }
}
