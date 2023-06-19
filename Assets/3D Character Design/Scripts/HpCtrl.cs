using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCtrl : MonoBehaviour
{
    [SerializeField] Image hpImage;
    [SerializeField] float maxHp = 1000;
    float currHp = 0;
    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;//遊戲一開始，當前血量等於最大血量。
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currHp / maxHp, 0.1f);
    }//fillAmount值由0~1決定，所以用當前血量/最大血量，就能得到fillAmount的值。用插值法，讓血量有漸變的效果，不是一次降到對應位置。
    void Damage(float damage) {
        currHp -= damage; //實際上傷害公式不會這麼簡單，可參考網路上戰鬥數值設計文章。
        if (currHp < 0) currHp = 0;//如果受到傷害使血量低於0，就等於血量歸零。
    }
}
