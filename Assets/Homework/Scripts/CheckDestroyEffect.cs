using UnityEngine;
using System.Collections;

public class CheckDestroyEffect : MonoBehaviour
{

    ParticleSystem[] pss;

    private void Start()
    {
        pss = GetComponentsInChildren<ParticleSystem>();
    }
    void Update()
    {
        for (int i = 0; i < pss.Length; i++)
        {
            if (pss[i].isPlaying)
            {
                return;
            }
        }

        Destroy(gameObject);
    }
}
