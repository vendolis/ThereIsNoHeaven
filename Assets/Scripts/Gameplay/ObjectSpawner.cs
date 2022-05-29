using System;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject objectToSpawn = default;

    private void Awake()
    {
        SpawnObject();
        Destroy(gameObject);
    }

    protected virtual void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}