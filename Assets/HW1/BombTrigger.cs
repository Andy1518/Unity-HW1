using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    //public ParticleSystem[] pss;
    public GameObject box;
    public GameObject bomb;
    public AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //pss = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool b = pss[0].isPlaying;
        //Debug.Log("is playing " + b);
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    foreach (ParticleSystem p in pss)
        //    {
        //        p.Play();
        //    }
        //}

        //else if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    foreach (ParticleSystem p in pss)
        //    {
        //        p.Stop();
        //    }
        //}

        //else if (Input.GetKeyDown(KeyCode.P))
        //{
        //    foreach (ParticleSystem p in pss)
        //    {
        //        p.Pause();
        //    }
        //}

        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    mainObject.SetActive(false);
        //    this.playEffects();
        //    audioPlayer.Play();
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        //Explosion(bomb);
        bomb.SetActive(true);
        audioPlayer.Play();
        Destroy(box);
        Destroy(bomb,5.0f);
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
