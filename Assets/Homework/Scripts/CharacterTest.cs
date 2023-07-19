using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTest : MonoBehaviour
{
    float speed = 100f;
    float velY = 0f;
    private void Start()
    {
        velY = GetComponent<Rigidbody>().velocity.y;
    }
    private void Update()
    {
        var vValue = Input.GetAxis("Vertical") * transform.forward;
        var hValue = Input.GetAxis("Horizontal") * transform.right;
        if (vValue != Vector3.zero || hValue != Vector3.zero) Debug.LogError("Unity-chan no move!s"); 

        Vector3 vel = (vValue + hValue) * speed * Time.deltaTime;
        vel.y = velY;
        GetComponent<Rigidbody>().velocity = vel;
    }
}
