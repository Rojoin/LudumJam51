using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class ClientController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float exitSpeed;
    Transform rayCastCenter;
    Transform backRayCastCenter;
    Transform rayCastDown;
    Transform Exit;
    GameObject servingPoint;
    Vector2 direction;

    Ray ray;

    private float raycastLength;
    [SerializeField] int rayDistance;
    [SerializeField] LayerMask clients;
    [SerializeField] LayerMask Lane;
    [SerializeField] Color wantedGuiso;

    [SerializeField] bool hasReceivedGuiso = false;
    [SerializeField] int SecondsToWait;
    bool isHappy = false;
    bool hasWaited = false;
    bool isWaiting = false;
    [SerializeField] private Color[] posibleColors;


    void Start()
    {
        wantedGuiso = posibleColors[UnityEngine.Random.Range(0, posibleColors.Length)];
        Debug.Log(wantedGuiso);
        servingPoint = GameObject.Find("ServingPoint");
        Exit = GameObject.Find("Exit").transform;
        rayCastCenter = transform.GetChild(0).transform;
        backRayCastCenter = transform.GetChild(1).transform;
        rayCastDown = transform.GetChild(2).transform;
        direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Physics2D.Raycast(rayCastCenter.position, direction, rayDistance, clients))
        {
            if (transform.position.x >= servingPoint.transform.position.x || hasReceivedGuiso || hasWaited)
            {
                transform.Translate(direction * speed * Time.deltaTime);

            }
            else if (!isWaiting && !isHappy)
            {
                StartCoroutine(Wait());

            }
            if (!Physics2D.Raycast(backRayCastCenter.position, Vector2.right, rayDistance, Lane))
            {
                direction = Vector2.down;
                rayCastCenter.position = new Vector3(0, -0.51f, 0);
            }
        }

        if (!isHappy && hasWaited)
        {
            // se enoja;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            speed = exitSpeed;
        }
        else if (isHappy && hasWaited)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }


        if (transform.position.y <= Exit.position.y)
            Destroy(gameObject);
    }


    void chooseColor()
    {
       
    }
    IEnumerator Wait()
    {
    
        isWaiting = true;
        float normalizedTime = 0;
        while (normalizedTime <=1 && !isGuisoCorrect())
        {
         

            normalizedTime += Time.deltaTime / SecondsToWait;
            if (isGuisoCorrect())
            {
                break;
            }
            yield return null;
        }

        hasWaited = true;
        isWaiting = false;
    }

    bool isGuisoCorrect()
    {
        RaycastHit2D hit; 
        Debug.Log("Checking");
        hit = Physics2D.Raycast(rayCastDown.transform.position, Vector2.down, raycastLength);
        if (
             (MathF.Abs(hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color.r - wantedGuiso.r) < .1f) &&
             (MathF.Abs(hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color.b - wantedGuiso.b) < .1f) &&
             (MathF.Abs(hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color.g - wantedGuiso.g) < .1f))
        {

            Debug.Log("Guiso Was Correct");
            isHappy = true;
            return true;
        }

        isHappy = false;
        return false;
    }

  
}
