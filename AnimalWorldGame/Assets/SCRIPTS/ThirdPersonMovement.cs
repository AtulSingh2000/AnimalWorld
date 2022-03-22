using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothtime = 0.1f;
    float turnSmoothVelocity;

    private Animator animator;

    [Header("Gravity")]
    public float gravity;
    public float currentGravity;
    public float maxGravity;
    public float constantGravity;
    private Vector3 gravityDirection;
    private Vector3 gravityMovement;
    
    private void Awake()
    {
        gravityDirection = Vector3.down;
    }
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        CalculateGravity();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            animator.SetBool("Run Axe", true);
            //animator.SetBool("Running", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized*speed *Time.deltaTime + gravityMovement);
        }
        else{
            animator.SetBool("Run Axe", false);
            //animator.SetBool("Running", false);
        }
    }

    private bool IsGrounded()
    {
        return controller.isGrounded;
    }
    private void CalculateGravity()
    {
        if(IsGrounded())
        {
            currentGravity = constantGravity;
        }
        else{
            if(currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        gravityMovement = gravityDirection * -currentGravity;
    }
}
