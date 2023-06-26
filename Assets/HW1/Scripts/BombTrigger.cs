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
    private void OnTriggerEnter(Collider other)
    {
        GameObject gEffect = Instantiate(bomb);
        gEffect.transform.position = transform.position;
        Destroy(gameObject);
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
