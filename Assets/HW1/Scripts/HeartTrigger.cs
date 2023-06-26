using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTrigger : MonoBehaviour
{
    public GameObject heart;
    public ParticleSystem particle;
    //public AudioSource audioPlayer;
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
        //Explosion(bomb);
        particle.Play();
        //audioPlayer.Play();
        Destroy(heart);
        //Destroy(particle, 5.0f);
        //playEffects();
    }
}
