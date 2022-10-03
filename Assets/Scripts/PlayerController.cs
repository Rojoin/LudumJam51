using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{



    [Header("Components")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D cl;
    [SerializeField] private Transform raycastCenter;
    [SerializeField] private ItemObject item;
    [SerializeField] private GameObject itemGameObject;



    [Header("Movement")]

    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private Vector2 movementDirection;

    private float horizontalPrev;
    private float verticalPrev;

    private bool flipCharacter => (rb.velocity.x > 0f && movementDirection.x < 0f) ||
                                  (rb.velocity.x < 0f && movementDirection.x > 0f);


    [Header("Collision")]

    [SerializeField] private float raycastLength = 0.3f;
    [SerializeField] private bool isFacingRight = true;



    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        itemGameObject = gameObject.transform.Find("Square").gameObject;
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

        movementDirection = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }

    public void GetEscapeInput(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GetInteractInput(InputAction.CallbackContext context)
    {

        Debug.Log("Interacciono");
        if (context.performed)
        {
            CheckCollision();
        }

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

        rb.MovePosition(rb.position + (movementDirection * movementAcceleration * Time.fixedDeltaTime));
        if (MathF.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(MathF.Sign(rb.velocity.x) * maxMoveSpeed, MathF.Sign(rb.velocity.y) * maxMoveSpeed);

    }

    #endregion


    #region COLLISIONS

    public void CheckCollision()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(raycastCenter.transform.position, Vector2.up, raycastLength);

        if (hit.collider != null)
        {

            if (hit.collider.gameObject.CompareTag("Collision"))
            {

                item = hit.collider.GetComponent<ItemObject>();
                itemGameObject.GetComponent<SpriteRenderer>().sprite = hit.collider.GetComponent<SpriteRenderer>().sprite;
                itemGameObject.GetComponent<SpriteRenderer>().color = hit.collider.GetComponent<SpriteRenderer>().color;

            }
            else if (hit.collider.gameObject.CompareTag("Pot"))
            {
                if (item != null)
                {

                    if (hit.collider.gameObject.GetComponent<Pot>().color != Color.black)
                    {
                        hit.collider.gameObject.GetComponent<Pot>().color = item.getColor;
                    }
                    else if (hit.collider.gameObject.GetComponent<Pot>().color != Color.red)
                    {
                        hit.collider.gameObject.GetComponent<Pot>().color = Color.red;
                    }
                }


            }
        }



    }



    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        Gizmos.DrawLine(raycastCenter.transform.position,
            raycastCenter.transform.position + Vector3.up * raycastLength);





    }

    #endregion


}