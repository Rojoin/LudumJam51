using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] public Color color;
    [SerializeField] private SpriteRenderer sprite;


   void Start()
   {
       sprite = GetComponent<SpriteRenderer>();
   }

    public Color getColor => color;
 
    public SpriteRenderer getSprite => sprite;
}
