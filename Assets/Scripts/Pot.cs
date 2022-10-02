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

    private int colorIndex;

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

       if (MathF.Abs(spriteRenderer.material.color.r - color.r)<.1f)
       {
           Debug.Log("Rojo");
           if (MathF.Abs(spriteRenderer.material.color.b - color.b) < .1f)
           {
           Debug.Log("Azul");
               if (MathF.Abs(spriteRenderer.material.color.g - color.g) < .1f)
               {
                   Debug.Log("Colores Parecidos");
               }
           }
       }
        //if (spriteRenderer.material.color == Color.LerpUnclamped(spriteRenderer.material.color, color, lerpTime * Time.deltaTime))
        //{
        //    Debug.Log("Termino");
        //}
        //else if (spriteRenderer.material.color != Color.LerpUnclamped(spriteRenderer.material.color, color, lerpTime * Time.deltaTime))
        //{
        //    Debug.Log("En Proceso");
        //    
        //}
       
    }
}
