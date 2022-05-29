using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    
    private IDestroyable destroyAble; 
    
    private void Start()
    {
        destroyAble = GetComponent<IDestroyable>();
        if (destroyAble != null)
            destroyAble.OnDie += HandleSpawn;
    }

    private void HandleSpawn()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        if (destroyAble != null)
            destroyAble.OnDie -= HandleSpawn;
    }
}
