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
            other.gameObject.SendMessage("GetStar", SendMessageOptions.DontRequireReceiver);//SendMessageOptions.DontRequireReceiver:通知遊戲執行方法，但不管對象身上有沒有此方法。
            //如果是RequireReceiver，通知完發現對象沒有這個方法後，Unity就會發生錯誤。
        }
        Destroy(gameObject);
    }
}
