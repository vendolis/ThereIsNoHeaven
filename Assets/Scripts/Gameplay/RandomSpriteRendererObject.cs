using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public class RandomSpriteRendererObject : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> sprites;

    private void Awake()
    {
        int randomIndex = UnityEngine.Random.Range(0, sprites.Count);
        Instantiate(sprites[randomIndex], transform.position, Quaternion.identity, transform);
    }
}