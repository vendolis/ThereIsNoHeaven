using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Target Manager")]
public class TargetManager : DescriptionBaseSO
{
    [SerializeField] private LayerMask targetableObjects;

    [Header("Targeters in Order of TargetType")]
    [SerializeField] private List<GameObject> targetMarkers;

    [Header("Listening On")] 
    [SerializeField] private TransformEventChannelSO targetingEvents;

    private Transform currentTarget;
    private GameObject currentMarker;
    
    
    public LayerMask GetTargetableObjects()
    {
        return targetableObjects;
    }

    private void OnEnable()
    {
        targetingEvents.OnEventRaised += processTarget;
    }

    private void OnDestroy()
    {
        targetingEvents.OnEventRaised -= processTarget;
    }

    private void processTarget(Transform arg0)
    {
        if (arg0 == null)
        {
            currentTarget = arg0;
            Destroy(currentMarker);
            return;
        }

        if (currentTarget == arg0)
        {
            return;
        }
        
        if (currentTarget != null)
        {
            Destroy(currentMarker);
        }
        
        currentTarget = arg0;

        CreateNewMarker(currentTarget, currentTarget.GetComponent<Targetable>().GetTargetType());

    }

    private void CreateNewMarker(Transform transform, TargetableType targetType)
    {
        currentMarker = Instantiate(targetMarkers[(int)targetType], transform);
    }
}
