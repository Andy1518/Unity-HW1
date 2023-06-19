using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTarget : MonoBehaviour
{
    CharacterCtrl3D characterCtrl;
    CapsuleCollider targetCollider;
    // Start is called before the first frame update
    void Start()
    {
        characterCtrl = GetComponentInParent<CharacterCtrl3D>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterCtrl.lockOn) {
            if (!targetCollider) targetCollider = characterCtrl.lockTarget.GetComponent<CapsuleCollider>();

            transform.LookAt(characterCtrl.lockTarget.position + targetCollider.center);
        }
        else targetCollider = null;
    }
}
