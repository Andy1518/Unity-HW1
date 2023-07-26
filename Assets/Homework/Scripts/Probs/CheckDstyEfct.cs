using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDstyEfct : MonoBehaviour
{
    ParticleSystem pss;

    private void Start()
    {
        pss = GetComponent<ParticleSystem>();
    }
    void Update()
    {

        if (pss.isPlaying)
        {
            return;
        }
        else
        Destroy(gameObject);
    }
}
