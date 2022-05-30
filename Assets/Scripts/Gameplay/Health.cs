using System;
using UnityEngine;

public class Health : MonoBehaviour, IDestroyable
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public event Action OnDie = delegate { };
    public event Action<GameObject> OnDestoryed;

    public event Action<int, int> OnHealthUpdated = delegate(int i, int i1) {  };

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnDestroy()
    {
        OnDestoryed?.Invoke(gameObject);
    }

    public void DealDamage(int damageAmount)
    {
        if (currentHealth == 0)
            return;

        var oldHealth = currentHealth;
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

        HandleHealthUpdated(oldHealth, currentHealth);
        
        if (currentHealth != 0)
            return;

        OnDie?.Invoke();

        Destroy(gameObject);
    }

    public void Kill()
    {
        DealDamage(currentHealth);
    }
    
    private void HandleHealthUpdated(int oldHealth, int newHealth)
    {
        OnHealthUpdated?.Invoke(newHealth, maxHealth);
    }
}
    
