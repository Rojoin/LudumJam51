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
    [SerializeField] private Animator animator;
    private BoxCollider2D cl;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    [SerializeField] private PhysicsMaterial2D frictionLess;


    [Header("Movement")]

    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float groundLinearDrag;
    [SerializeField] private Vector2 movementDirection;
    private float horizontalPrev;
    private float verticalPrev;
    private bool flipCharacter => (rb.velocity.x > 0f && movementDirection.x < 0f) ||
                                  (rb.velocity.x < 0f && movementDirection.x > 0f);


    [Header("Collision")]

    [SerializeField] private float groundRaycastLength = 0.3f;
    [SerializeField] private Vector3 groundRaycastOffset;
    [SerializeField] private bool isFacingRight = true;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        //animator.SetBool("Crouching", crouch);
        //animator.SetBool("Jumping", jump);
        //animator.SetBool("Attaching", attached);
        //animator.SetBool("OnGround", onGround);
        //animator.SetBool("Walking", MathF.Abs(horizontalDirection) >= 0.7f);
    }
    private void FixedUpdate()
    {
       
       
        CheckCollision();
        MoveCharacter();
       

        if (isFacingRight && movementDirection.x < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && movementDirection.x > 0f)
        {
            Flip();
        }
    }

    #region INPUTS

  
    public void GetHorizontalInput(InputAction.CallbackContext context)
    {

        Debug.Log("Movimiento");
        movementDirection =new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);



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
       // dust.Play();
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

        rb.MovePosition(rb.position + (movementDirection*movementAcceleration*Time.fixedDeltaTime));
        Debug.Log("moverse");
            if (MathF.Abs(rb.velocity.x) > maxMoveSpeed)
                rb.velocity = new Vector2(MathF.Sign(rb.velocity.x) * maxMoveSpeed, MathF.Sign(rb.velocity.y) * maxMoveSpeed);

    }

    #endregion


    #region GROUNDCOLLISIONS

    public void CheckCollision()
    {
        
        
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