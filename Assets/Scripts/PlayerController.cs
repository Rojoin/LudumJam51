using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{



    [Header("Components")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform raycastCenter;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask slopeLayer;
    [SerializeField] private Animator animator;
    private BoxCollider2D cl;
    private HingeJoint2D hj;
    public GameObject checkPoint;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    [SerializeField] private PhysicsMaterial2D frictionLess;


    [Header("Movement")]

    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float groundLinearDrag;
    [SerializeField] private float horizontalDirection;
    private float verticalDirection;
    private float horizontalPrev;
    private float verticalPrev;
    private bool flipCharacter => (rb.velocity.x > 0f && horizontalDirection < 0f) ||
                                  (rb.velocity.x < 0f && horizontalDirection > 0f);




    [Header("Collision")]

    [SerializeField] private float groundRaycastLength = 0.3f;
    [SerializeField] private Vector3 groundRaycastOffset;
    [SerializeField] private bool isFacingRight = true;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
        hj = gameObject.GetComponent<HingeJoint2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        animator.SetBool("Crouching", crouch);
        animator.SetBool("Jumping", jump);
        animator.SetBool("Attaching", attached);
        animator.SetBool("OnGround", onGround);
        animator.SetBool("Walking", MathF.Abs(horizontalDirection) >= 0.7f);
    }
    private void FixedUpdate()
    {
       
       
        CheckCollision();
        MoveCharacter();
       

        if (isFacingRight && horizontalDirection < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontalDirection > 0f)
        {
            Flip();
        }
    }

    #region INPUTS

  
    public void GetHorizontalInput(InputAction.CallbackContext context)
    {


        horizontalDirection = context.ReadValue<Vector2>().x;

        verticalDirection = context.ReadValue<Vector2>().y;




        // if (context.performed)
        // {
        //     StopCoroutine(airDragEnumerator());
        // }
        // if (context.canceled)
        // {
        //     StartCoroutine(airDragEnumerator());
        //
        // }



    }

    public void GetEscapeInput(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GetCrouchInput(InputAction.CallbackContext context)
    {
        

    }
    #endregion

    #region AESTHETIC
    void CreateDust()
    {
        dust.Play();
    }
    private void Flip()
    {
        CreateDust();
        isFacingRight = !isFacingRight;
        var localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    #endregion


    #region MOVE


    private void MoveCharacter()
    {
        if (!crouch && !attached && !onSlopes)
        {

            rb.AddForce(new Vector2(horizontalDirection, 0) * movementAcceleration);

            if (MathF.Abs(rb.velocity.x) > maxMoveSpeed)
                rb.velocity = new Vector2(MathF.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);


            rb.drag = 20.0f;
        }
        else if (onSlopes)
        {
            Debug.Log("llegue");
            rb.AddForce(new Vector2(-horizontalDirection * slopeNormal.x * movementAcceleration, slopeNormal.y * -horizontalDirection * movementAcceleration) * 2);

            if (MathF.Abs(rb.velocity.x) > maxMoveSpeed)
                rb.velocity = new Vector2(MathF.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            rb.drag = 20.0f;
        }
        else if (crouch)
        {
            rb.AddForce(new Vector2(horizontalDirection, 0) * movementAcceleration / 2);
            if (MathF.Abs(rb.velocity.x) > maxMoveSpeed / 2)
                rb.velocity = new Vector2(MathF.Sign(rb.velocity.x) * (maxMoveSpeed / 2), rb.velocity.y);
        }
        else if (attached)
        {

            if (horizontalDirection > 0)
            {
                VineSwing(true);
            }
            else if (horizontalDirection < 0)
            {
                VineSwing(false);
            }
            if (verticalDirection > 0)
            {
                Debug.Log("Agarrado");
                VineSlide(true);
            }
            else if (verticalDirection < 0)
            {

                VineSlide(false);
            }
        }
    }

    #endregion


    #region GROUNDCOLLISIONS

    public void CheckCollision()
    {
        onGround = Physics2D.Raycast(raycastCenter.transform.position * groundRaycastLength, Vector2.down, groundRaycastLength, groundLayer) ||
                   Physics2D.Raycast(raycastCenter.transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);
        onSlopes = Physics2D.Raycast(raycastCenter.transform.position * groundRaycastLength, Vector2.down, groundRaycastLength, slopeLayer) ||
                     Physics2D.Raycast(raycastCenter.transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, slopeLayer);

        RaycastHit2D hit = Physics2D.Raycast(raycastCenter.transform.position * groundRaycastLength, Vector2.down,
            groundRaycastLength, slopeLayer);
        if (hit)
        {
            slopeNormal = Vector2.Perpendicular(hit.normal).normalized;
            slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
        }

        if (onGround && onSlopes)
        {
            coyoteCounter = coyoteTime;
            disregard = null;
            rb.sharedMaterial = fullFriction;

        }
        else if (!onGround && onSlopes)
        {
            coyoteCounter = coyoteTime;
            disregard = null;
            rb.sharedMaterial = fullFriction;

        }
        else if (onGround && !onSlopes)
        {
            coyoteCounter = coyoteTime;
            disregard = null;
            rb.sharedMaterial = frictionLess;

        }
        else
            coyoteCounter -= Time.deltaTime;

        if (onSlopes && horizontalDirection != 0)
        {
            rb.sharedMaterial = frictionLess;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(raycastCenter.transform.position, raycastCenter.transform.position + Vector3.down * groundRaycastLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastCenter.transform.position - groundRaycastOffset, raycastCenter.transform.position - groundRaycastOffset + Vector3.down * groundRaycastLength);
    }

    #endregion


}