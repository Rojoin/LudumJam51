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
    [SerializeField] public float SecondsToWait;
    bool isHappy = false;
    bool hasWaited = false;
    bool isWaiting = false;
    [SerializeField]public int randomPotion;
    [SerializeField]  public int randomChar;
    [SerializeField] private Color[] posibleColors;
    [SerializeField] private Sprite[] posiblePotions;
    [SerializeField] private Sprite[] posibleCharactersLeft;
    [SerializeField] private Sprite[] posibleCharactersFront;
    [SerializeField] private Sprite[] posibleExpresionsHappy;
    [SerializeField] private Sprite[] posibleExpresionsSad;
    [SerializeField] private GameObject desiredPotion;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject character;

    void Start()
    {
        character = transform.GetChild(5).gameObject;
        //randomPotion = UnityEngine.Random.Range(0, posibleColors.Length);
        wantedGuiso = posibleColors[randomPotion];
        //randomChar = UnityEngine.Random.Range(0, posibleCharactersFront.Length);
        character.GetComponent<SpriteRenderer>().sprite = posibleCharactersLeft[randomChar];

        dialogueBox = transform.GetChild(3).gameObject;
        dialogueBox.SetActive(false);
        desiredPotion = transform.GetChild(4).gameObject;
        dialogueBox.SetActive(false);
        servingPoint = GameObject.Find("ServingPoint");
        Exit = GameObject.Find("Exit").transform;
        rayCastCenter = transform.GetChild(0).transform;
        backRayCastCenter = transform.GetChild(1).transform;
        rayCastDown = transform.GetChild(2).transform;
        direction = Vector2.left;
        ChooseDialogue();
        StartCoroutine(SayWantedGuiso());
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

            if (isWaiting)
            {
                character.GetComponent<SpriteRenderer>().sprite = posibleCharactersFront[randomChar];
            }
          
            if (!Physics2D.Raycast(backRayCastCenter.position, Vector2.right, rayDistance, Lane))
            {
                direction = Vector2.down;
                rayCastCenter.position = new Vector3(0, -0.51f, 0);
                character.GetComponent<SpriteRenderer>().sprite = posibleCharactersFront[randomChar];

            }
        }

      
        if (!isHappy && hasWaited)
        {
            // se enoja;


            desiredPotion.GetComponent<SpriteRenderer>().sprite = posibleExpresionsSad[randomChar];
            ShowDialogue();

            speed = exitSpeed;
        }
        else if (isHappy && hasWaited)
        {

            ShowDialogue();
            desiredPotion.GetComponent<SpriteRenderer>().sprite = posibleExpresionsHappy[randomChar];
            
        }


        if (transform.position.y <= Exit.position.y)
            Destroy(gameObject);
    }

    void ChooseDialogue()
    {
        desiredPotion.GetComponent<SpriteRenderer>().sprite = posiblePotions[randomPotion];
    }
    void ShowDialogue()
    {
        dialogueBox.SetActive(true);
        desiredPotion.SetActive(true);

    }
    void quitDialogue()
    {
        dialogueBox.SetActive(false);
        desiredPotion.SetActive(false);

    }
    IEnumerator SayWantedGuiso()
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1)
        {

            normalizedTime += Time.deltaTime / 2;

        }

        normalizedTime = 0;
        while (normalizedTime <= 1)
        {
            ShowDialogue();

            normalizedTime += Time.deltaTime / 3;

            yield return null;
        }
        quitDialogue();
    }
    IEnumerator Wait()
    {

        isWaiting = true;
    
        float normalizedTime = 0;
        while (normalizedTime <= 1 && !isGuisoCorrect())
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
        character.GetComponent<SpriteRenderer>().sprite = posibleCharactersLeft[randomChar];
    }

    bool isGuisoCorrect()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(rayCastDown.transform.position, Vector2.down, raycastLength);
        if (
             (MathF.Abs(hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color.r - wantedGuiso.r) < .1f) &&
             (MathF.Abs(hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color.b - wantedGuiso.b) < .1f) &&
             (MathF.Abs(hit.collider.gameObject.GetComponent<SpriteRenderer>().material.color.g - wantedGuiso.g) < .1f))
        {


            isHappy = true;
            return true;
        }

        isHappy = false;
        return false;
    }

    public bool getIsHappy => isHappy;
}
