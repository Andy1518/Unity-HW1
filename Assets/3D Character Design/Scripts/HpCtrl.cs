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
        currHp = maxHp;//�C���@�}�l�A��e��q����̤j��q�C
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currHp / maxHp, 0.1f);
    }//fillAmount�ȥ�0~1�M�w�A�ҥH�η�e��q/�̤j��q�A�N��o��fillAmount���ȡC�δ��Ȫk�A����q�����ܪ��ĪG�A���O�@�����������m�C
    void Damage(float damage) {
        currHp -= damage; //��ڤW�ˮ`�������|�o��²��A�i�ѦҺ����W�԰��ƭȳ]�p�峹�C
        if (currHp < 0) currHp = 0;//�p�G����ˮ`�Ϧ�q�C��0�A�N�����q�k�s�C
    }
}
