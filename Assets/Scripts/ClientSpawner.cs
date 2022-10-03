using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] bool instaSpawn;
    [SerializeField]float timeSpawn;
    [SerializeField]float secondsToWait;
    Vector3 pos;
    bool gameIsRunning;
    void Start()
    {
        pos = transform.position;
        gameIsRunning = true;
        StartCoroutine(Spawn(instaSpawn));
    }
    IEnumerator Spawn(bool instaSpawn)
    {
        if (instaSpawn)
        {
            while (gameIsRunning)
            {
                GameObject Client =Instantiate(prefab, pos, transform.rotation);
               Client.GetComponent<ClientController>().randomChar =  UnityEngine.Random.Range(0, 3);
               Client.GetComponent<ClientController>().randomPotion =  UnityEngine.Random.Range(0, 5);
               Client.GetComponent<ClientController>().SecondsToWait = secondsToWait;
                yield return new WaitForSeconds(timeSpawn);
            }
        }
        else
        {
            while (gameIsRunning)
            {
                yield return new WaitForSeconds(timeSpawn);
                Instantiate(prefab, pos, transform.rotation);
            }
        }
    }
}
