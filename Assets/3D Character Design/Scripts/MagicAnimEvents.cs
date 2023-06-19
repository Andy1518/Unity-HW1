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
    void CreateFireball() { //注意名稱一定要取跟前面設定動畫事件的名字一樣。
        fireballOnHand.SetActive(true);//把手上的火球粒子系統預置物打開顯示。
    }
    void ShootFireball(GameObject fireballPrefab) {
        fireballOnHand.SetActive(false);//把手上的火球粒子系統預置物關掉顯示。
        Instantiate(fireballPrefab, fireballOnHand.transform.position, //動態生成火球預置物，從手上放火球的座標生成，火球攻擊方向指向玩家。
            Quaternion.LookRotation(aiCtrl.lockTarget.position + Vector3.up * 1.3f - fireballOnHand.transform.position));
    }//LookRotation裡面要放向量。由AICharacterCtrl3D程式裡面的lockTarget取得玩家座標，用玩家座標減去火球生成座標，得到由火球指向玩家的向量，
     //再由該向量計算旋轉角度出來。如果直接使用玩家座標會變成打在位於地上的軸心點，
     //因此玩家座標要再往上加個大約1.3公尺，才會是身體的位置，也就是+ Vector3.up * 1.3f的用意。
    void CreateFireMeteor(GameObject fireMeteorPrefab) {
        Instantiate(fireMeteorPrefab, aiCtrl.lockTarget.position, Quaternion.identity);
    }//動態生成火隕石，位置是玩家位置，不需要旋轉。
}
