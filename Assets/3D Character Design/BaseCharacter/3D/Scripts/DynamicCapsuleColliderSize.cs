using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class DynamicCapsuleColliderSize : MonoBehaviour
{
    [SerializeField] Transform headEnd;
    CapsuleCollider capsuleCollider;

    void Start() {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 localHeadEnd = transform.InverseTransformPoint(headEnd.position);
        //Vector3 localFootEnd = transform.InverseTransformPoint(transform.position);
        //capsuleCollider.height = localHeadEnd.y - localFootEnd.y;
        capsuleCollider.height = headEnd.position.y - transform.position.y;
        capsuleCollider.center = new Vector3(0, capsuleCollider.height / 2, 0);
    }
}