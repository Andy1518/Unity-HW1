using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float damage = 180;//預設此武器傷害為180。
    Animator animator;
    private void Start()
    {
        animator = transform.root.GetComponent<Animator>();//抓此武器之根物體(也就是戰士)的Animator元件並把記憶體位置存在animator。
    }
    private void OnTriggerEnter(Collider other){//不能事先GetComponent，因為它還不知道會砍到誰，所以只能在這裡即時抓對象元件。
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")){//NameToLayer:把Layer名字是Enemy的轉成該Layer編號出來。當碰到物件是在Enemy這層，即觸發。
            if(Mathf.Approximately(animator.GetFloat("AttackCurve"), 1)) {//當戰士揮砍動畫之Curve的值等於1，才傳出傷害判定訊息。                
                //float在電腦上判定很不精確，所以通常這時候會使用Approximately來判定該值是否相近於1，如果有就會直接回傳true了，而不會寫==1來判定。
            other.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);//SendMessageOptions.DontRequireReceiver:通知遊戲執行Damage方法，但不管對象身上有沒有此方法。
            }//如果是RequireReceiver，通知完發現對象沒有這個方法後，Unity就會發生錯誤。
        }
    }
}
