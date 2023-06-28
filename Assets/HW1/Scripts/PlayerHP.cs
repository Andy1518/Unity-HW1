using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] Image hpImage;
    [SerializeField] float maxHp = 150;
    //public GameObject replayButton;
    float currHp = 0;
    Animator animator;
    //CharacterCtrl3D characterCtrl;
    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;//遊戲一開始，當前血量等於最大血量。
        animator = GetComponent<Animator>();
        //characterCtrl = GetComponent<CharacterCtrl3D>();
        //Time.timeScale = 1;
        //replayButton.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currHp / maxHp, 0.1f);
        //fillAmount值由0~1決定，所以用當前血量/最大血量，就能得到fillAmount的值。用插值法，讓血量有漸變的效果，不是一次降到對應位置。        
        //if (currHp == 0)
        //{
        //    gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        //    animator.SetTrigger("Die");
        //    characterCtrl.isDead = true;
        //    Time.timeScale = Mathf.Lerp(1.0f, 0, 0.8f);
        //}
    }
    void Hurt(float hurt)
    {
        //characterCtrl.isHurt = true;
        animator.SetTrigger("Hurt");
        currHp -= hurt;
        if (currHp < 0)
        {
            currHp = 0;//如果受到傷害使血量低於0，就等於血量歸零。            
        }
    }
    void Heal(float heal)
    {
        currHp += heal;
        if (currHp > maxHp)
        {
            currHp = maxHp;           
        }
    }
}
