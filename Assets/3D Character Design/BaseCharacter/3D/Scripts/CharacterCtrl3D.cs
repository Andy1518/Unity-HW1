using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class CharacterCtrl3D : MonoBehaviour
{
    [SerializeField] LayerMask lockOnFilter;
    [SerializeField] GameObject freeLookCamera, lockOnCamera;
    [SerializeField] float lockOnRadius = 10;
    [SerializeField] float lockOnAngle = 15;
    [SerializeField] bool alwaysLockOn = false;

    Character3D character;
    CinemachineVirtualCameraBase vcFreeLook, vcLockTarget;
    Transform camTransform;
    public Transform lockTarget {
        get;
        private set;
    }
    public bool lockOn {
        get;
        private set;
    }    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        character = GetComponent<Character3D>();
        if(freeLookCamera) vcFreeLook = freeLookCamera.GetComponent<CinemachineVirtualCameraBase>();
        if(lockOnCamera) vcLockTarget = lockOnCamera.GetComponent<CinemachineVirtualCameraBase>();
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camForward = Vector3.ProjectOnPlane(camTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(camTransform.right, Vector3.up).normalized;
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetMouseButtonDown(2)) {            
            if (lockOn) {
                lockTarget = null;
                lockOn = false;
            }
            else {                
                Collider[] enemys = Physics.OverlapSphere(transform.position, lockOnRadius, lockOnFilter);
                if(enemys.Length > 0) {
                    IEnumerable<Collider> colliders =
                        from e in enemys
                        where Vector3.Angle(camForward, Vector3.ProjectOnPlane(e.transform.position - camTransform.position, Vector3.up)) < lockOnAngle
                        orderby Vector3.Distance(transform.position, e.transform.position) ascending                                
                        select e;
                    if (colliders.Count() > 0) {
                        lockTarget = colliders.First().transform;
                        lockOn = true;
                    }
                }
            }
        }

        if(lockOn) {            
            if (!alwaysLockOn) {
                if (Vector3.Distance(transform.position, lockTarget.position) > lockOnRadius + 0.5f) {
                    lockTarget = null;
                    lockOn = false;
                }
            }
        }
                
        if (lockOn) {
            if (freeLookCamera && lockOnCamera) {
                vcFreeLook.Priority = 0;
                vcLockTarget.Priority = 10;
                vcFreeLook.ForceCameraPosition(lockOnCamera.transform.position, lockOnCamera.transform.rotation);
            }
        }
        else {
            if (freeLookCamera && lockOnCamera) {
                vcLockTarget.Priority = 0;
                vcFreeLook.Priority = 10;
                vcLockTarget.ForceCameraPosition(freeLookCamera.transform.position, freeLookCamera.transform.rotation);
            }
        }

        Vector3 move;
        if(lockOn)
            move = camForward;        
        else 
            move = (camRight * axisInput.x + camForward * axisInput.y).normalized;

        character.Move(move, axisInput, Input.GetButtonDown("Jump"), lockOn);
    }
}