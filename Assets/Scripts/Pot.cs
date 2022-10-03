using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Range(0, 1)] public float lerpTime;
    public float lerpSpeed;

    public Color color;

    public bool isGuisoReady = false;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       spriteRenderer.material.color = Color.LerpUnclamped(spriteRenderer.material.color, color,lerpTime*Time.deltaTime);

   

    }

   public bool getIsGuisoReady=> isGuisoReady;
}
