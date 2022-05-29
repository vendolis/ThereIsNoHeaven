using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{

    [SerializeField] private Transform targeterSource;
    [SerializeField] private TargetManager currentTargetManager;
    
    [Header("Sending On")]
    [SerializeField] private TransformEventChannelSO targetedEventChannel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTarget();
    }

    void GetTarget()
    {
        RaycastHit2D[] directionCheck = Physics2D.RaycastAll(targeterSource.transform.position, 
            targeterSource.transform.up, 
            100f, 
            currentTargetManager.GetTargetableObjects());


        foreach (var hitObject in directionCheck)
        {
            if (hitObject.transform.GetComponent<Targetable>())
            {
                targetedEventChannel.RaiseEvent(hitObject.transform);
                return;
            }
        }
        
        targetedEventChannel.RaiseEvent(null);
    }
}
