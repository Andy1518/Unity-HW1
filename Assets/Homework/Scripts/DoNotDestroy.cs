using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
/*
    static DoNotDestroy initiator;
    private void Awake()
    {
        if (initiator == null)
        {
            initiator = this;
            DontDestroyOnLoad(this);
        }
        else if (this != initiator)
        {//�p�G���O�̤@�}�l������
            Destroy(gameObject);//�N�R�F�L
        }
    }
*/
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
