using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] bool instaSpawn;
    [SerializeField]float timeSpawn;
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
                Instantiate(prefab, pos, transform.rotation);
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
