using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetableType
{
    Other=0,
    Mineable=1,
    Shootable=2,
    Interactable=3
}

public class Targetable : MonoBehaviour
{
    [SerializeField] private TargetableType type = TargetableType.Other;

    public TargetableType GetTargetType()
    {
        return type;
    }
    
}
