using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class exitPoint : MonoBehaviour
{
    private BoxCollider2D cl;

    public int happyCustomers = 0;
    public int angryCustomers = 0;
    public int maxAngryCustomers = 0;
    public bool loseGame = false;
    void Start()
    {
        cl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       LoseCondition();
    }

    void LoseCondition()
    {
        if (angryCustomers > maxAngryCustomers)
        {
            loseGame = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Init");
     
        if (other.gameObject.GetComponent<ClientController>().getIsHappy)
        {
            Debug.Log("Happy");
            happyCustomers++;
        }
        else if (!other.gameObject.GetComponent<ClientController>().getIsHappy)
        {
            angryCustomers++;
        }
    }
}
