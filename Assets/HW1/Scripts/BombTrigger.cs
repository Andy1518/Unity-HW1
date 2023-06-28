using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    //public ParticleSystem[] pss;
    //public GameObject box;
    public GameObject bomb;
    //public AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //pss = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] float hurt = 30;
    private void OnTriggerEnter(Collider other)
    {
        GameObject gEffect = Instantiate(bomb);
        gEffect.transform.position = transform.position;
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("Hurt", hurt, SendMessageOptions.DontRequireReceiver);//SendMessageOptions.DontRequireReceiver:通知遊戲執行Hurt方法，但不管對象身上有沒有此方法。
            //如果是RequireReceiver，通知完發現對象沒有這個方法後，Unity就會發生錯誤。
            Destroy(gameObject);
        }
        //Explosion(bomb);
        //bomb.SetActive(true);
        //audioPlayer.Play();
        //Destroy(box);
        //Destroy(bomb,5.0f);
        //playEffects();
    }
    /*
    void Explosion(GameObject ExplosionPrefab)
    {
        Instantiate(ExplosionPrefab, box.transform.position, Quaternion.identity);
    }
    */

    //void playEffects()
    //{
    //    foreach (ParticleSystem p in pss)
    //    {
    //        p.Play();
    //    }
    //}
}
