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
            other.gameObject.SendMessage("Hurt", hurt, SendMessageOptions.DontRequireReceiver);//SendMessageOptions.DontRequireReceiver:�q���C������Hurt��k�A�����޹�H���W���S������k�C
            //�p�G�ORequireReceiver�A�q�����o�{��H�S���o�Ӥ�k��AUnity�N�|�o�Ϳ��~�C
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
