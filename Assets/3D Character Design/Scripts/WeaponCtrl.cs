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
        if (Input.GetKeyDown(KeyCode.Tab)) {//按鍵盤Tab鍵。
            //isFighting = !isFighting;依照當前狀態的相反值覆寫回去。
            //animator.SetBool("IsFighting", isFighting);把值寫進去IsFighting。
            //以上為配合原動畫流程設計之程式寫法，該方法會導致角色在收拿武器時整個人停下來跑動畫。以下為新做法：
            if (isFighting) {//當IsFighting為true。
                animator.SetTrigger("PutWeapon");//正在攻擊狀態，觸發收武器動畫。
            }
            else {//當IsFighting為false。
                animator.SetTrigger("GetWeapon");//沒在攻擊狀態，觸發拿武器動畫。
            }
        }
    }
    void GetWeapon() {
        isFighting = true;
        animator.SetBool("IsFighting", true);
        weapon.SetParent(weaponHolderOnHand);//把Weapon的父物件改成WeaponHolderOnHand指向的WeaponHolder，就是前面事先拖曳好的手上的WeaponHolder空白物件。
        weapon.localPosition = Vector3.zero;//將武器的座標歸零，等於Reset。
        weapon.localRotation = Quaternion.identity;//將武器的旋轉角度歸零，等於Reset。
    }
    void PutWeapon() {
        isFighting = false;
        animator.SetBool("IsFighting", false);
        weapon.SetParent(weaponHolderOnBack);//把Weapon的父物件改成WeaponHolderOnBack指向的WeaponHolder，就是前面事先拖曳好的背上的WeaponHolder空白物件。
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.identity;
    }
    void WeaponTrailEffect(int index) {
        Instantiate(weaponTrails[index], attackHolders[index]);//動態生成。把weaponTrails依序動態生成到attackHolders。
    }
}
