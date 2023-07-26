using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTirgger : MonoBehaviour
{
    public GameObject getStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject gEffect = Instantiate(getStar);
        gEffect.transform.position = transform.position;
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("GetStar", SendMessageOptions.DontRequireReceiver);//SendMessageOptions.DontRequireReceiver:�q���C�������k�A�����޹�H���W���S������k�C
            //�p�G�ORequireReceiver�A�q�����o�{��H�S���o�Ӥ�k��AUnity�N�|�o�Ϳ��~�C
        }
        Destroy(gameObject);
    }
}
