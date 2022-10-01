using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] bool instaSpawn;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
        StartCoroutine(Spawn(instaSpawn));
    }
    IEnumerator Spawn(bool instaSpawn)
    {
        if (instaSpawn)
        {
            Instantiate(prefab, pos, transform.rotation);
            yield return new WaitForSeconds(10f);
        }
        else
        {
        yield return new WaitForSeconds(10f);
        Instantiate(prefab, pos, transform.rotation);
        }
    }
}
