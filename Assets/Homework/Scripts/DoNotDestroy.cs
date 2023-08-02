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
        {//如果不是最一開始的物件
            Destroy(gameObject);//就刪了他
        }
    }
*/
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
