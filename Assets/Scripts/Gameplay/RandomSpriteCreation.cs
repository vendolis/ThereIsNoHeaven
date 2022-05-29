using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class RandomSpriteCreation : MonoBehaviour
{
    [SerializeField] private List<Sprite> listOfSprites = default;

    private SpriteRenderer mySpriteRenderer = default;
    
    private void Awake()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        var rand = new Random();

        mySpriteRenderer.sprite = listOfSprites[rand.Next(0, listOfSprites.Count - 1)];
    }
}
