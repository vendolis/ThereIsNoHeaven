
using System;
using UnityEngine;

public interface IDestroyable
{
    public event Action OnDie;
    public event Action<GameObject> OnDestoryed;
}
