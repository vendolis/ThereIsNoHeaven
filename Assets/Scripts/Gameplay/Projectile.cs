using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour, IDestroyable
{

    [SerializeField] private Collider2D projectileCollider;
    [SerializeField] private LayerMask collisionLayer;
 


    [SerializeField] private int damageAmount = 100;
    [SerializeField] private float projectileSpeed = 20;
    [SerializeField] private float timeToLive = 2f;

    
    public event Action OnDie = delegate {  };
    public event Action<GameObject> OnDestoryed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        
        if(timeToLive < 0)
            Destroy(gameObject);
        
        transform.Translate(0,projectileSpeed * Time.deltaTime,0);
    }

    private void OnDestroy()
    {
        OnDestoryed?.Invoke(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!projectileCollider.IsTouchingLayers(collisionLayer))
            return;

        Health hitObjectHealth = col.gameObject.GetComponent<Health>();
        
        if (hitObjectHealth)
        {
            hitObjectHealth.DealDamage(damageAmount);
        }
        
        OnDie.Invoke();
        
        Destroy(gameObject);
    }
    
}
