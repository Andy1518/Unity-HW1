using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3D : MonoBehaviour
{

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] float jumpForce = 8;
    [SerializeField] float gravityScale = 2;

    [SerializeField] float moveSpeed = 5.0f;

    Animator animator;
    Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
        rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Move(Vector3 move, Vector2 axis, bool jump, bool lockOn) {
        GroundCheck();

        if (animator.GetBool("OnGround")) {
            if (jump) {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else {
                move = transform.InverseTransformDirection(move);
                ApplyRotation(move);

                if (lockOn) {
                    animator.SetFloat("xSpeed", axis.x, 0.5f, Time.deltaTime);
                    animator.SetFloat("zSpeed", axis.y, 0.5f, Time.deltaTime);
                }
                else {
                    animator.SetFloat("xSpeed", 0, 0.1f, Time.deltaTime);
                    animator.SetFloat("zSpeed", move.z * axis.magnitude, 0.5f, Time.deltaTime);
                }
            }
            //Vector3 velocity = axis * moveSpeed * move;
            //velocity.y = rigidbody.velocity.y;
            //rigidbody.velocity = velocity;
            //transform.localPosition += move * Time.fixedDeltaTime;
        }
        animator.SetFloat("ySpeed", rigidbody.velocity.y);
    }

    void GroundCheck() {
        if (Physics.OverlapSphere(transform.position, groundCheckRadius, whatIsGround).Length > 0)
            animator.SetBool("OnGround", true);        
        else
            animator.SetBool("OnGround", false);        
    }

    void ApplyRotation(Vector3 move) {
        float turnSpeed = Mathf.Lerp(180, 360, move.z);
        float turnAmount = Mathf.Atan2(move.x, move.z);
        transform.Rotate(0, turnSpeed * turnAmount * Time.deltaTime, 0);
    }

    private void FixedUpdate() {
        if(rigidbody.useGravity)
            rigidbody.AddForce(Physics.gravity * gravityScale - Physics.gravity);
        else
            rigidbody.AddForce(Physics.gravity * gravityScale);
    }

    //private void OnAnimatorMove()
    //{
    //    if (animator.GetBool("OnGround"))
    //    {
    //        Vector3 velocity = animator.deltaPosition / Time.deltaTime;
    //        //Vector3 velocity = transform.forward * moveSpeed;
    //        velocity.y = rigidbody.velocity.y;
    //        rigidbody.velocity = velocity;
    //    }
    //}
}