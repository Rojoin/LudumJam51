using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private SpriteRenderer renderer;

    [Range(0, 1)] public float lerpTime;
    public float lerpSpeed;

    public Color color;

    private int colorIndex;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, color,lerpTime*Time.deltaTime);
    }
}
