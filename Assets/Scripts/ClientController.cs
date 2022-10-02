using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float exitSpeed;
    Transform rayCastCenter;
    Transform backRayCastCenter;
    Transform Exit;
    GameObject servingPoint;
    Vector2 direction;

    Ray ray;
    [SerializeField] int rayDistance;
    [SerializeField] LayerMask clients;
    [SerializeField] LayerMask Lane;
    [SerializeField] Color wantedGuiso;

    [SerializeField] bool hasReceivedGuiso = false;
    [SerializeField] int SecondsToWait;
    bool hasWaited = false;
    bool isWaiting = false;
    void Start()
    {
        servingPoint = GameObject.Find("ServingPoint");
        Exit = GameObject.Find("Exit").transform;
        rayCastCenter = transform.GetChild(0).transform;
        backRayCastCenter = transform.GetChild(1).transform;
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
            else if (!isWaiting && !hasWaited)
            {
                StartCoroutine(Wait());

            }
            if (!Physics2D.Raycast(backRayCastCenter.position, Vector2.right, rayDistance, Lane))
            {
                direction = Vector2.down;
                rayCastCenter.position = new Vector3(0, -0.51f, 0);
            }
        }

        if (hasWaited)
        {
            // se enoja;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            speed = exitSpeed;
        }


        if (transform.position.y <= Exit.position.y)
            Destroy(gameObject);
    }


    IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(SecondsToWait);
        hasWaited = true;
        isWaiting = false;
    }

    bool isGuisoCorrect()
    {



        return false;

    }
}
