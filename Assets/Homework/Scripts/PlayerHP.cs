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
        currHp = maxHp;//�C���@�}�l�A��e��q����̤j��q�C
        animator = GetComponent<Animator>();
        //characterCtrl = GetComponent<CharacterCtrl3D>();
        //Time.timeScale = 1;
        //replayButton.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currHp / maxHp, 0.1f);
        //fillAmount�ȥ�0~1�M�w�A�ҥH�η�e��q/�̤j��q�A�N��o��fillAmount���ȡC�δ��Ȫk�A����q�����ܪ��ĪG�A���O�@�����������m�C        
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
            currHp = 0;//�p�G����ˮ`�Ϧ�q�C��0�A�N�����q�k�s�C            
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
